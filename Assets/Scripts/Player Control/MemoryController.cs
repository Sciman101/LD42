using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MemoryController : MonoBehaviour {

    const int NUM_SLOTS = 7;//Number of available memory slots
    const int HEIGHT = 490;

    public MemData[] memory;//The robot's memory
    int selectedSlot;
    int sPrev;
    bool confirm;

    int codeCount;

    //Array for UI elements
    [Header("UI")]
    public UIListHelper memList;
    public Image uiSelector, uiIndicator;
    public GameObject popup;
    [Header("Misc.")]
    public GameObject pixelRegion;
    Text popupText;
    public AudioClip alert, delete, getData;

    public Transform visualizer;

    RoomController currentRoom;
    SoundHelper sound;

    //Initialize array
	void Start () {
        memory = new MemData[NUM_SLOTS];

        //Initialize UI elements
        UpdateUI();

        uiIndicator.gameObject.SetActive(false);
        visualizer.gameObject.SetActive(false);

        sound = GetComponent<SoundHelper>();

        popupText = popup.GetComponentInChildren<Text>();
        popup.SetActive(false);
    }

    //Pick up memdata
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            //Try and pick up their thing
            MemoryPickup pickup = other.GetComponent<MemoryPickup>();
            int slot = FirstEmptySlot();
            if (pickup && slot != -1)
            {
                if (!MemoryContains(pickup.data))
                {
                    memory[slot] = pickup.data;
                    UpdateUI();
                    pickup.Hide();
                    if (pickup.data.name.IndexOf("L.") != -1)
                    {
                        string text = "Launch Codes Aqquired!";
                        
                        if (++codeCount == 3)
                        {
                            text += " Return back to the start to complete mission";
                        }
                        ShowPopup(text);
                    }
                    sound.Play(getData);
                }
                else
                {
                    ShowPopup("You already have that data!");
                }
            }
            else
            {
                ShowPopup("Out of free memory!");
            }
        }else if (other.tag == "Room")
        {
            //We have entered a new room
            currentRoom = other.GetComponent<RoomController>();
            AddRoomToMemory();
            MoveIndicator();

        }else if (other.tag == "Door")
        {
            DoorController d = other.GetComponent<DoorController>();
            if (!MemoryContains(d.keyCode))
            {
                ShowPopup("You need the <i>" + d.keyCode.name + "</i> to access this!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Room")
        {
            currentRoom = null;//Leave room
            uiIndicator.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        //Select slot
        int s = (int)Input.GetAxis("SelectSlot") - (int)Input.GetAxis("Mouse ScrollWheel");
        if (s != 0 && sPrev == 0)
        {

            selectedSlot += s;
            if (selectedSlot < 0) selectedSlot = NUM_SLOTS - 1;
            if (selectedSlot >= NUM_SLOTS) selectedSlot = 0;

            uiSelector.rectTransform.anchoredPosition = new Vector2(0, -78 * selectedSlot);
            confirm = false;

            HighlightRoomOnMap();
        }
        sPrev = s;
        

        //Delete memory
        if (Input.GetButtonDown("Delete"))
        {
            MemData m = memory[selectedSlot];
            if (m != null)
            {
                bool cancel = false;
                if (m.associatedRoom != null)
                {
                    
                    if (m.associatedRoom == currentRoom)
                    {
                        ShowPopup("Cannot delete map data for current room");
                        cancel = true;
                    }
                    else
                    {
                        Instantiate(pixelRegion, m.associatedRoom.transform.position + Vector3.up * 2, Quaternion.identity);
                        m.associatedRoom.Invoke("Hide", 0.5f);//The room is hidden, now
                        confirm = true;
                        //m.associatedRoom.SetVisible(false);
                    }
                }
                if (!cancel)
                {
                    if (!confirm)
                    {
                        ShowPopup("Are you sure you want to delete non-map data?");
                        confirm = true;
                    }
                    else
                    {
                        memory[selectedSlot] = null;
                        UpdateUI();
                        AddRoomToMemory();
                        HidePopup();
                        sound.Play(delete);
                        confirm = false;
                    }
                }
            }
        }

        //Scan a room
        /*if (Input.GetButtonDown("Scan"))
        {
            if (currentRoom != null && !MemoryContains(currentRoom.roomData))
            {
                int slot = FirstEmptySlot();
                if (slot != -1)
                {
                    memory[slot] = currentRoom.roomData;
                    currentRoom.SetVisible(true);
                    UpdateUI();
                    MoveIndicator();
                }
                else
                {
                    ShowPopup("Out of free memory!");
                }
            }
        }*/
    }

    //Get the first available empty slot
    int FirstEmptySlot()
    {
        int r = -1;
        for (int i=0;i<NUM_SLOTS;i++)
        {
            if (memory[i] == null)
            {
                r = i;
                break;
            }
        }
        return r;
    }

    //Does our memory already have something?
    public bool MemoryContains(MemData data)
    {
        for (int i = 0; i < NUM_SLOTS; i++)
        {
            if (memory[i] == data)
            {
                return true;
            }
        }
        return false;
    }

    void AddRoomToMemory()
    {
        //Try and add room to memory
        if (currentRoom != null && !MemoryContains(currentRoom.roomData))
        {
            int slot = FirstEmptySlot();
            if (slot != -1)
            {
                memory[slot] = currentRoom.roomData;
                currentRoom.SetVisible(true);
                UpdateUI();
                sound.Play();
            }
            else
            {
                ShowPopup("Out of memory space for map data!");
            }
        }
        else
        {
            if (currentRoom != null) currentRoom.SetVisible(true);
        }
    }

    //Update UI
    void UpdateUI()
    {
        //Populate list of icons
        for (int i=0;i<NUM_SLOTS;i++)
        {
            memList.SetSlot(memory[i], i);
        }
        HighlightRoomOnMap();
    }

    void HighlightRoomOnMap()
    {
        //Room visualizer
        if (memory[selectedSlot] != null)
        {
            RoomController room = memory[selectedSlot].associatedRoom;
            if (room != null)
            {
                visualizer.gameObject.SetActive(true);
                visualizer.position = room.transform.position + Vector3.up * 5;
            }
            else
            {
                visualizer.gameObject.SetActive(false);
            }
        }
        else visualizer.gameObject.SetActive(false);
    }

    void MoveIndicator()
    {
        bool found = false;
        for (int i = 0; i < NUM_SLOTS; i++)
        {
            if (memory[i] == currentRoom.roomData)
            {
                uiIndicator.gameObject.SetActive(true);
                uiIndicator.rectTransform.anchoredPosition = new Vector2(0, -78 * i);
                found = true;
                break;
            }
        }
        if (!found) uiIndicator.gameObject.SetActive(false);
    }

    public void ShowPopup(string text)
    {
        popup.SetActive(true);
        popupText.text = text;
        Invoke("HidePopup", 3);
        sound.Play(alert);
    }
    void HidePopup()
    {
        popup.SetActive(false);
    }

}
