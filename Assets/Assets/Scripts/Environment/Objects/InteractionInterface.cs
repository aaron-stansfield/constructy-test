using UnityEngine;

public interface InteractionInterface
{
    //virtual public void pickUpObj(bool ongoing){
    //}

    abstract public void interactWithObj(Transform transform);

    virtual public void drop(){

    }

}
