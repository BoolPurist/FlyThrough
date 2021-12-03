using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NiceGraphicLibrary;

namespace FlyThrough
{
  public class SAudioCollectionPlayer : SingletonComponent<SAudioCollectionPlayer>
  {

    [SerializeField]
    private AudioSource ButtonClickSound;
   
    public void PlayButtonSound() => ButtonClickSound.Play();

  }

}