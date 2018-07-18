using System.Collections.Generic;
using UnityEngine;

namespace ZsCLJX
{
    public class DownRiseGridRelation : MonoBehaviour
    {
        public List<DownRiseGrid> mRelationList1;
        public List<DownRiseGrid> mRelationList2;

        public List<DownRiseGrid> GetMovePath(DownRiseGrid varCur, DownRiseGrid varTarget)
        {
            int tempIndex01 = mRelationList1.IndexOf(varCur);
            int tempIndex02 = mRelationList1.IndexOf(varTarget);

            int tempIndex11 = mRelationList2.IndexOf(varCur);
            int tempIndex12 = mRelationList2.IndexOf(varTarget);

            if (tempIndex01 >= 0 && tempIndex02 >= 0)
            {
                return GetMovePathOnGroup(tempIndex01, tempIndex02, mRelationList1);
            }
            else if (tempIndex11 >= 0 && tempIndex12 >= 0)
            {
                return GetMovePathOnGroup(tempIndex11, tempIndex12, mRelationList2);
            }
            else
            {
                if (tempIndex01 >= 0 && tempIndex12 >= 0)
                {
                    List<DownRiseGrid> tempList = new List<DownRiseGrid>();
                    int tempIndex = tempIndex01;
                    while (tempIndex != 0)
                    {
                        tempIndex--;
                        tempList.Add(mRelationList1[tempIndex]);
                    }
                    while (tempIndex < tempIndex12)
                    {
                        tempIndex++;
                        tempList.Add(mRelationList2[tempIndex]);
                    }
                    return tempList;
                }
                else if (tempIndex11 >= 0 && tempIndex02 >= 0)
                {
                    List<DownRiseGrid> tempList = new List<DownRiseGrid>();
                    int tempIndex = tempIndex11;
                    while (tempIndex != 0)
                    {
                        tempIndex--;
                        tempList.Add(mRelationList2[tempIndex]);
                    }
                    while (tempIndex < tempIndex02)
                    {
                        tempIndex++;
                        tempList.Add(mRelationList1[tempIndex]);
                    }
                    return tempList;
                }
                return null;
            }
        }

        private List<DownRiseGrid> GetMovePathOnGroup(int varIndex1, int varIndex2, List<DownRiseGrid> varRelationList)
        {
            List<DownRiseGrid> tempList = new List<DownRiseGrid>();
            int tempIndex = varIndex1;
            while (tempIndex != varIndex2)
            {
                if (tempIndex > varIndex2)
                {
                    tempIndex--;
                }
                else if (tempIndex < varIndex2)
                {
                    tempIndex++;
                }
                tempList.Add(varRelationList[tempIndex]);
            }
            return tempList;
        }
    }
}
