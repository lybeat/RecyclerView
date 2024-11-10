using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace NaiQiu.Framework.View
{
    public class ItemListView : MonoBehaviour
    {
        [SerializeField] private ViewHolder itemViewHolder;

        [SerializeField] private Direction direction = Direction.Horizontal;
        public Direction Direction
        {
            get => direction;
            set
            {
                direction = value;
                if (recyclerView != null)
                {
                    recyclerView.Direction = direction;
                }
            }
        }

        [SerializeField] private Alignment alignment = Alignment.Center;
        public Alignment Alignment
        {
            get => alignment;
            set
            {
                alignment = value;
                if (recyclerView != null)
                {
                    recyclerView.Alignment = alignment;
                }
            }
        }

        [SerializeField] private Vector2 spacing = Vector2.zero;
        public Vector2 Spacing
        {
            get => spacing;
            set
            {
                spacing = value;
                if (recyclerView != null)
                {
                    recyclerView.Spacing = spacing;
                }
            }
        }

        [SerializeField] private Vector2 padding = Vector2.zero;
        public Vector2 Padding
        {
            get => padding;
            set
            {
                padding = value;
                if (recyclerView != null)
                {
                    recyclerView.Padding = padding;
                }
            }
        }

        [SerializeField] private bool scroll = true;
        public bool Scroll
        {
            get => scroll;
            set => scroll = value;
        }

        private RecyclerView recyclerView;
        private Adapter<ItemData> itemAdapter;

        private GameObject contentObj;

        private void Awake()
        {
            recyclerView = gameObject.AddComponent<RecyclerView>();
            recyclerView.Direction = direction;
            recyclerView.Alignment = alignment;
            recyclerView.Spacing = spacing;
            recyclerView.Padding = padding;
            recyclerView.Scroll = scroll;
            recyclerView.Templates = new ViewHolder[1] { itemViewHolder };
            CreateContentObj();
            itemAdapter = new Adapter<ItemData>(recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager());
            itemAdapter.SetOnItemClick(data =>
            {
                if (GameManager.UIManager.Open("ItemDetailsDialog", out ItemDetailsDialog dialog))
                {
                    dialog.SetItemData(data);
                }
            });
        }

        private void CreateContentObj()
        {
            contentObj = new("Content");
            contentObj.transform.SetParent(recyclerView.transform, false);
            contentObj.AddComponent<Image>();
            contentObj.AddComponent<Mask>().showMaskGraphic = false;
            contentObj.GetComponent<RectTransform>().Fill();
        }

        public void SetItemList(List<ItemData> itemDatas)
        {
            itemAdapter.SetList(itemDatas);
        }

        public void SetItemData(ItemData itemData)
        {
            itemAdapter.SetList(new List<ItemData>() { itemData });
        }
    }

    public class ItemData
    {
        public string name;
        public string icon;
        public string type;
        public int rarity;
        public string content;
        public string source;
        public string purpose;

        public ItemData(string name, string icon, string type, int rarity, string content, string source, string purpose)
        {
            this.name = name;
            this.icon = icon;
            this.type = type;
            this.rarity = rarity;
            this.content = content;
            this.source = source;
            this.purpose = purpose;
        }
    }
}
