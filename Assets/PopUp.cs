using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void Hide()
    {
        _animator.SetTrigger("Hide");
    }

    public void Close()
    {
        gameObject.SetActive(true);
    }
}
