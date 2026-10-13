using Naninovel;
using UnityEngine;

public class BackTitle : MonoBehaviour
{
    private bool isBackToTitle = false;

    public async void BackToTitle()
    {
        if(isBackToTitle||MySceneManager.Instance.isLoading) return;

        try
        {
            isBackToTitle = true;

            //ÉZÅ[ÉuÇµÇƒèâä˙âªÇµÇƒÇ®Ç≠
            await Engine.GetService<IStateManager>().AutoSave();
            await Engine.GetService<IStateManager>().ResetState();

            await MySceneManager.Instance.ChangeScene("Title");
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
        }
        finally
        {
            isBackToTitle = false;
        }   
    }
}
