using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupOkCancel : PopupBase
{


    [Space(20)]
    [SerializeField] private TMP_Text txtTitle;
    [SerializeField] private TMP_Text txtContent;


    public void Show(string title, string content, Action cbOK = null, Action cbCancel = null)
    {
        // print($"ShowPopupOk title: {title} -  content: {content}");
        txtTitle.text = title.ToUpper();
        txtContent.text = content;
        Show(cbOK, cbCancel);
    }


}
