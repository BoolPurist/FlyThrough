using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NiceGraphicLibrary.Component.GUI;

namespace FlyThrough
{
  
  public class PlaySoundOnClick : ObserverForButtonClick
  {
    protected override void OnButtonClicked()
    {
      SAudioCollectionPlayer.Instance.PlayButtonSound();
    }
  }
}
