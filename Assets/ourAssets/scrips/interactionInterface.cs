
using System.Collections;
using System.Transactions;
using UnityEngine;

public interface interactionInterface
{
    //virtual public void pickUpObj(bool ongoing){
    //}

    abstract public void pickUpObj(Transform transform);

    virtual public void drop(){

    }

}
