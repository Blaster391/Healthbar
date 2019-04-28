using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource _genericSource;
    [SerializeField]
    private AudioClip _battleStartSound;
    [SerializeField]
    private AudioClip _shopOpenSound;
    [SerializeField]
    private AudioClip _buySound;
    [SerializeField]
    private AudioClip _buttonPressedSound;
    [SerializeField]
    private AudioClip _buttonSuccessSound;
    [SerializeField]
    private AudioClip _buttonFailedSound;

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
    private AudioClip _enemySlideSound;
    [SerializeField]
    private AudioClip _enemyHurtSound;
    [SerializeField]
    private AudioClip _enemyDeadSound;
    [SerializeField]
    private AudioSource _enemySoundSource;

    [SerializeField]
    private AudioClip _defaultBGM;
    [SerializeField]
    private AudioSource _bgmSource;

    private void OnEnable()
    {
        GameMaster.Find<GameTimeManager>().OnTick += PlayMetronomeSound;
        GameMaster.Find<GameTimeManager>().OnHalfTick += PlayMetronomeSound;
        GameMaster.Find<PlayerScript>().OnPlayerDamaged += PlayPlayerHurt;
        GameMaster.Find<PlayerScript>().OnPlayerKilled += PlayPlayerDead;

        GameMaster.Find<EnemyScript>().OnEnemyDamaged += PlayEnemyHurt;
        GameMaster.Find<EnemyScript>().OnEnemyKilled += PlayEnemyDead;

        GameMaster.Find<BattleManager>().OnNextBattle += SwapBGM;
    }

    private void OnDisable()
    {
        GameMaster.Find<GameTimeManager>().OnTick -= PlayMetronomeSound;
        GameMaster.Find<GameTimeManager>().OnHalfTick -= PlayMetronomeSound;
        GameMaster.Find<PlayerScript>().OnPlayerDamaged -= PlayPlayerHurt;
        GameMaster.Find<PlayerScript>().OnPlayerKilled -= PlayPlayerDead;

        GameMaster.Find<EnemyScript>().OnEnemyDamaged -= PlayEnemyHurt;
        GameMaster.Find<EnemyScript>().OnEnemyKilled -= PlayEnemyDead;

        GameMaster.Find<BattleManager>().OnNextBattle -= SwapBGM;
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
        _bgmSource.clip = _playerDeadSound;
        _bgmSource.Play();
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

    void Start()
    {
        _bgmSource.clip = _defaultBGM;
        _bgmSource.Play();
    }

    void SwapBGM()
    {
        float currentTime = _bgmSource.time;
        if(_bgmSource.clip == _defaultBGM)
        {
            currentTime = 0;
        }
        _bgmSource.clip = GameMaster.Find<BattleManager>().GetCurrentBattle().BattleBGM;
        _bgmSource.Play();
        _bgmSource.time = currentTime;
    }

    public void EnemySlide()
    {
        _enemySoundSource.clip = _enemySlideSound;
        _enemySoundSource.Play();
    }

    public void ShopOpen()
    {
        _genericSource.clip = _shopOpenSound;
        _genericSource.Play();
    }

    public void BattleStart()
    {
        _genericSource.clip = _battleStartSound;
        _genericSource.Play();
    }

    public void EnemyEnterSound()
    {
        if(GameMaster.Find<BattleManager>().GetCurrentWave().EnterSound != null)
        {
            _enemySoundSource.clip = GameMaster.Find<BattleManager>().GetCurrentWave().EnterSound;
            _enemySoundSource.Play();
        }
    }

    public void BuySound()
    {
        _genericSource.clip = _buySound;
        _genericSource.Play();
    }

    public void ButtonPressed()
    {
        _genericSource.clip = _buttonPressedSound;
        _genericSource.Play();
    }

    public void ButtonSuccess()
    {
        _genericSource.clip = _buttonSuccessSound;
        _genericSource.Play();
    }

    public void ButtonFailed()
    {
        _genericSource.clip = _buttonFailedSound;
        _genericSource.Play();
    }
}
