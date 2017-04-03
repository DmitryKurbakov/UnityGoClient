using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scenes.web.Scripts
{
    public class Pass : MonoBehaviour
    {
        public void OnPassButton()
        {
            if (CreateConnection.isPause) return;
            Debug.Log("Pass Button Pressed");
            StartCoroutine(PassButtonClicked());
        }

        private IEnumerator PassButtonClicked()
        {
            // yield return new WaitForSeconds(1);
       
            CreateConnection.socket.Emit(CreateConnection.player.isBlack ? "BLACK_PASSED" : "WHITE_PASSED");

            yield return new WaitForSeconds(0);

        }

    }
}
