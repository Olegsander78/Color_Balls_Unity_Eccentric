using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveItem : MonoBehaviour
{
    public int Level;
    public float Radius;
    [SerializeField] private Text _levelText;

    [SerializeField] private Transform _visualtransform;
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private SphereCollider _trigger;

    public Rigidbody Rigidbody;
    public bool IsDead;
    [SerializeField] private Animator _animator;

    [ContextMenu("IncreaseLevel")]
    public void Increaselevel()
    {
        Level++;
        SetLevel(Level);
        _animator.SetTrigger("IncreaseLevel");
        _trigger.enabled = false;
        Invoke(nameof(EnableTrigger), 0.1f);
    }

    public virtual void SetLevel(int level)
    {
        Level = level;
        //��������� ����� �� ����
        int number = (int)Mathf.Pow(2, level + 1);
        string numberString = number.ToString();
        _levelText.text = numberString;

        Radius = Mathf.Lerp(0.4f, 0.7f, level / 10f);
        Vector3 ballScale = Vector3.one * Radius * 2f;
        _visualtransform.localScale = ballScale;
        _collider.radius = Radius;
        _trigger.radius = Radius + 0.1f;
    }

    void EnableTrigger()
    {
        _trigger.enabled = true;
    }

    //������������� Item � �����

    public void SetupInTube()
    {
        //��������� ������
        _trigger.enabled = false;
        _collider.enabled = false;
        Rigidbody.isKinematic = true;
        Rigidbody.interpolation = RigidbodyInterpolation.None;
    }

    public void Drop()
    {
        //�������� ������ ����
        _trigger.enabled = true;
        _collider.enabled = true;
        Rigidbody.isKinematic = false;
        Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        //������������� �� ������
        transform.parent = null;
        //������ �������� ����, ����� ����� �������
        Rigidbody.velocity = Vector3.down * 1.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsDead)
        {
            return;
        }

        if (other.attachedRigidbody)
        {
            ActiveItem otheritem = other.attachedRigidbody.GetComponent<ActiveItem>();
            if (otheritem)
            {
                if (Level == otheritem.Level && !otheritem.IsDead)
                {
                    CollapseManager.Instance.Collapse(this, otheritem);
                }
            }
        }
    }

    public void Disable()
    {
        _trigger.enabled = false;
        _collider.enabled = false;
        Rigidbody.isKinematic = true;
        IsDead = true;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
