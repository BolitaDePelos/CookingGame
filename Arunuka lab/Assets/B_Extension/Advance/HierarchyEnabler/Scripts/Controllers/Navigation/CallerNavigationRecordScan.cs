using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using B_Extensions.HierarchyStates;

public class CallerNavigationRecordScan : MonoBehaviour
{
    public void CallRecord() => NavigationRecordController.Instance.ScanElements();
}
