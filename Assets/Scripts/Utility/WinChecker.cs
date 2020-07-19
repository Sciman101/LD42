using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinChecker : MonoBehaviour {

    public MemData[] requirements;
    public int count;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            int reqCount = 0;
            MemoryController mem = other.GetComponent<MemoryController>();
            for (int i=0;i<requirements.Length;i++)
            {
                if (mem.MemoryContains(requirements[i])) reqCount++;
            }

            if (reqCount >= count)
            {
                //Handle win condition
                GameController.instance.EndGame();
            }
            else
            {
                mem.ShowPopup("You still need " + (count - reqCount) + " launch codes! Return here once you've found them");
            }

        }
    }
}
