using System;
using System.Collections;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Other
{
    /// <summary>
    ///     Reusable Coroutine packer. Creates <see cref="UnityEngine.Coroutine"/>.
    /// </summary>
    public sealed class SmartCoroutine : CustomYieldInstruction
    {
        [NotNull] private readonly MonoBehaviour _coroutineLauncher;
        [CanBeNull] private readonly Action _onEnd;
        [CanBeNull] private readonly Action _onStart;
        [NotNull] private readonly Func<IEnumerator> _routine;

        /// <summary>
        ///     A coroutine, that will be started after <i>this one</i> ends.
        /// </summary>
        [CanBeNull] public SmartCoroutine Next;

        public SmartCoroutine(
            [NotNull] MonoBehaviour coroutineLauncher,
            [NotNull] Func<IEnumerator> routine,
            [CanBeNull] Action onStart = null,
            [CanBeNull] Action onEnd = null)
        {
            _coroutineLauncher = coroutineLauncher;
            _routine = routine;
            _onStart = onStart;
            _onEnd = onEnd;
        }

        public SmartCoroutine([NotNull] MonoBehaviour coroutineLauncher,
            [NotNull] SmartCoroutine coroutine, [CanBeNull] Action onStart = null,
            [CanBeNull] Action onEnd = null) : this(
            coroutineLauncher, coroutine.StartCoroutine, onStart, onEnd)
        {
        }

        public bool Finished { get; private set; } = true;

        /// <summary>
        ///     The last coroutine, that will be started in <see cref="Next"/> chain.
        /// </summary>
        public SmartCoroutine Last => Next is null ? this : Next.Last;

        public override bool keepWaiting => !Finished;

        public event Action OnStart;
        public event Action OnFinished;

        public static SmartCoroutine StackCoroutines(MonoBehaviour coroutineLauncher,
            Func<IEnumerator>[] routines,
            bool cycle = false) =>
            StackCoroutines(routines.Select(routine => new SmartCoroutine(
                        coroutineLauncher,
                        routine))
                    .ToArray(),
                cycle);

        // ReSharper disable once MemberCanBePrivate.Global
        public static SmartCoroutine StackCoroutines(SmartCoroutine[] coroutines,
            bool cycle = false)
        {
            for (var i = 0; i < coroutines.Length - 1; i++)
                coroutines[i].Next = coroutines[i + 1];

            if (cycle) coroutines[^1].Next = coroutines[0];

            return coroutines[0];
        }

        public SmartCoroutine Start()
        {
            Finished = false;
            _onStart?.Invoke();
            OnStart?.Invoke();
            _coroutineLauncher.StartCoroutine(ActualCoroutine());

            return this;
        }

        /// <summary>
        ///     Starts if possible (prohibited if process is still running).
        /// </summary>
        /// <returns>Succeed or not</returns>
        public bool TryRestart()
        {
            if (!Finished) return false;

            Start();
            return true;
        }

        private IEnumerator ActualCoroutine()
        {
            yield return _coroutineLauncher.StartCoroutine(_routine());

            Finished = true;
            OnFinished?.Invoke();
            _onEnd?.Invoke();

            if (Next is not null)
                yield return _coroutineLauncher
                    .StartCoroutine(Next.Start());
        }

        private IEnumerator StartCoroutine()
        {
            Start();
            yield return this;
        }
    }
}