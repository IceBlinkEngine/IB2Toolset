using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Reflection;

namespace IB2miniToolset
{
    public class SortableBindingList<T> : BindingList<T>
    {
        private ArrayList sortedList;
        private ArrayList unsortedItems;
        private bool isSortedValue;

        public SortableBindingList()
        {
        }
        public SortableBindingList(IList<T> list)
        {
            foreach (object o in list)
            {
                this.Add((T)o);
            }
        }
        protected override bool SupportsSearchingCore
        {
            get
            {
                return true;
            }
        }
        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            PropertyInfo propInfo = typeof(T).GetProperty(prop.Name);
            T item;

            if (key != null)
            {
                for (int i = 0; i < Count; ++i)
                {
                    item = (T)Items[i];
                    if (propInfo.GetValue(item, null).Equals(key))
                        return i;
                }
            }
            return -1;
        }
        public int Find(string property, object key)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            PropertyDescriptor prop = properties.Find(property, true);

            if (prop == null)
                return -1;
            else
                return FindCore(prop, key);
        }
        protected override bool SupportsSortingCore
        {
            get { return true; }
        }
        protected override bool IsSortedCore
        {
            get { return isSortedValue; }
        }

        ListSortDirection sortDirectionValue;
        PropertyDescriptor sortPropertyValue;

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            sortedList = new ArrayList();

            // Check to see if the property type we are sorting by implements
            // the IComparable interface.
            Type interfaceType = prop.PropertyType.GetInterface("IComparable");

            if (interfaceType != null)
            {
                // If so, set the SortPropertyValue and SortDirectionValue.
                sortPropertyValue = prop;
                sortDirectionValue = direction;

                unsortedItems = new ArrayList(this.Count);

                // Loop through each item, adding it the the sortedItems ArrayList.
                foreach (Object item in this.Items)
                {
                    sortedList.Add(prop.GetValue(item));
                    unsortedItems.Add(item);
                }
                // Call Sort on the ArrayList.
                sortedList.Sort();
                T temp;

                // Check the sort direction and then copy the sorted items
                // back into the list.
                if (direction == ListSortDirection.Descending)
                    sortedList.Reverse();

                for (int i = 0; i < this.Count; i++)
                {
                    int position = Find(prop.Name, sortedList[i]);
                    if (position != i)
                    {
                        temp = this[i];
                        this[i] = this[position];
                        this[position] = temp;
                    }
                }

                isSortedValue = true;

                // Raise the ListChanged event so bound controls refresh their
                // values.
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
            else
                // If the property type does not implement IComparable, let the user
                // know.
                throw new NotSupportedException("Cannot sort by " + prop.Name +
                    ". This" + prop.PropertyType.ToString() +
                    " does not implement IComparable");
        }
        protected override void RemoveSortCore()
        {
            int position;
            object temp;

            if (unsortedItems != null)
            {
                for (int i = 0; i < unsortedItems.Count; )
                {
                    position = this.Find("LastName",
                        unsortedItems[i].GetType().
                        GetProperty("LastName").GetValue(unsortedItems[i], null));
                    if (position > 0 && position != i)
                    {
                        temp = this[i];
                        this[i] = this[position];
                        this[position] = (T)temp;
                        i++;
                    }
                    else if (position == i)
                        i++;
                    else
                        unsortedItems.RemoveAt(i);
                }
                isSortedValue = false;
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }
        public void RemoveSort()
        {
            RemoveSortCore();
        }
        protected override PropertyDescriptor SortPropertyCore
        {
            get { return sortPropertyValue; }
        }
        protected override ListSortDirection SortDirectionCore
        {
            get { return sortDirectionValue; }
        }
    }
}
