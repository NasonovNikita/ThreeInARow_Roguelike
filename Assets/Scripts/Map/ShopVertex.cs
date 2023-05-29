using UnityEngine.SceneManagement;

public class ShopVertex : Vertex
{
    public override void OnArrive()
    {
        SceneManager.LoadScene("Shop");
    }
}