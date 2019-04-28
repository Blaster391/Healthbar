using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip _metronomeSound;
    [SerializeField]
    private AudioSource _metronomeSoundSource;

    [SerializeField]
    private AudioClip _playerHurtSound;
    [SerializeField]
    private AudioClip _playerDeadSound;
    [SerializeField]
    private AudioSource _playerSoundSource;

    [SerializeField]
    private AudioClip _enemyHurtSound;
    [SerializeField]
    private AudioClip _enemyDeadSound;
    [SerializeField]
    private AudioSource _enemySoundSource;

    private void OnEnable()
    {
        GameMaster.Find<GameTimeManager>().OnTick += PlayMetronomeSound;
        GameMaster.Find<GameTimeManager>().OnHalfTick += PlayMetronomeSound;
        GameMaster.Find<PlayerScript>().OnPlayerDamaged += PlayPlayerHurt;
        GameMaster.Find<PlayerScript>().OnPlayerKilled += PlayPlayerDead;

        GameMaster.Find<EnemyScript>().OnEnemyDamaged += PlayEnemyHurt;
        GameMaster.Find<EnemyScript>().OnEnemyKilled += PlayEnemyDead;
    }

    private void OnDisable()
    {
        GameMaster.Find<GameTimeManager>().OnTick -= PlayMetronomeSound;
        GameMaster.Find<GameTimeManager>().OnHalfTick -= PlayMetronomeSound;
        GameMaster.Find<PlayerScript>().OnPlayerDamaged -= PlayPlayerHurt;
    }

    private void PlayMetronomeSound()
    {
        _metronomeSoundSource.clip = _metronomeSound;
        _metronomeSoundSource.Play();
    }

    private void PlayPlayerHurt(int lol)
    {
        _playerSoundSource.clip = _playerHurtSound;
        _playerSoundSource.Play();
    }

    private void PlayPlayerDead()
    {
        _playerSoundSource.clip = _playerDeadSound;
        _playerSoundSource.Play();
    }

    private void PlayEnemyHurt(int lol)
    {
        _enemySoundSource.clip = _enemyHurtSound;
        _enemySoundSource.Play();
    }

    private void PlayEnemyDead()
    {
        _enemySoundSource.clip = _enemyDeadSound;
        _enemySoundSource.Play();
    }
}
