import os



cnt = 0
for root, dirs, files in os.walk(".."):
    for file in files:
        if file[-3:] == ".cs":
            f = open(os.path.join(root, file), 'r')
            for line in f.readlines():
                if line.rstrip().replace("{", "").replace("}", "").replace(" ", "") != "":
                    cnt += 1
            f.close()
print(cnt)
input()