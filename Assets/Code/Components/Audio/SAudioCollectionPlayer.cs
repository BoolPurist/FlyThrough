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
    [SerializeField]
    private AudioSource HittingWall;
    [SerializeField]
    private AudioSource PassThrough;

    public void PlayButtonSound() => ButtonClickSound.Play();
    public void PlayHitWallSound() => HittingWall.Play();
    public void PlayPassThrough() => PassThrough.Play();

  }

}