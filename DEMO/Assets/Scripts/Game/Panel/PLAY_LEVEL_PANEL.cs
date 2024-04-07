// -- IMPORTS

using TMPro;
using UnityEngine;
using CORE_MODULE;
using TEXT_MESH = TMPro.TextMeshProUGUI;

// -- TYPES

namespace GAME_MODULE
{
    #if UNITY_EDITOR
    [ UPDATE( "LEVEL HERO", "*" ) ]
    #endif

    public class PLAY_LEVEL_PANEL : PANEL
    {
        // -- CONSTANTS

        public const float
            MinimapRadius = 65.0f;

        // -- ATTRIBUTES

        public AudioClip
            ScoreAudioClip;
        public Transform
            HealthBarHealthTransform;
        public TEXT_MESH
            RemainingTimeTextMesh,
            ScoreTextMesh;
        public float
            Health;
        public int
            Score;
        public TEXT
            ScoreText;
        public int
            RemainingMinuteCount,
            RemainingSecondCount;
        public TEXT
            RemainingTimeText;
        public int
            RemainingEnemyCount;
        public ENEMY[]
            EnemyArray;
        public Transform
            MinimapGroundTransform,
            MinimapHeroTransform;
        public GameObject
            MinimapEnemyPrefabGameObject;
        public GameObject[]
            MinimapEnemyGameObjectArray;
        public bool[]
            IsMinimapEnemyVisibleArray;

        // -- OPERATIONS

        public new void OnEnable(
            )
        {
            int
                enemy_index;

            base.OnEnable();

            GAME.HideCursor();

            HealthBarHealthTransform = FindSubTransformByName( "HealthBarHealth" );
            RemainingTimeTextMesh = FindSubEntityByName<TEXT_MESH>( "RemainingTimeText" );
            ScoreTextMesh = FindSubEntityByName<TEXT_MESH>( "ScoreText" );

            Health = -1.0f;

            Score = -1;
            ScoreText = new TEXT();

            RemainingMinuteCount = -1;
            RemainingSecondCount = -1;
            RemainingTimeText = new TEXT();

            RemainingEnemyCount = 0;

            EnemyArray = LEVEL.Instance.EnemyArray;

            MinimapGroundTransform = FindSubTransformByName( "MinimapGround" );
            MinimapHeroTransform = FindSubTransformByName( "MinimapHero" );

            MinimapEnemyPrefabGameObject = ( GameObject )Resources.Load( "Prefabs/MinimapEnemy" );
            MinimapEnemyPrefabGameObject.SetActive( false );
            MinimapEnemyGameObjectArray = new GameObject[ EnemyArray.Length ];
            IsMinimapEnemyVisibleArray = new bool[ EnemyArray.Length ];

            for ( enemy_index = 0;
                  enemy_index < EnemyArray.Length;
                  ++enemy_index )
            {
                MinimapEnemyGameObjectArray[ enemy_index ]
                    = ( GameObject )Object.Instantiate( MinimapEnemyPrefabGameObject, Vector3.zero, Quaternion.identity, MinimapGroundTransform );
            }
        }

        // ~~

        public void UpdateHealthBar(
            )
        {
            float
                health;

            health = LEVEL.Instance.Hero.Health;

            if ( health < 0.0f )
            {
                health = 0.0f;
            }

            if ( Health != health )
            {
                Health = health;

                HealthBarHealthTransform.localScale = new Vector3( Health * 0.01f, 1.0f, 1.0f );
            }
        }

        // ~~

        public void UpdateRemainingTime(
            )
        {
            float
                remaining_time;
            int
                remaining_minute_count,
                remaining_second_count;

            remaining_time = LEVEL.Instance.RemainingTime;

            if ( remaining_time < 0.0f )
            {
                remaining_time = 0.0f;
            }

            remaining_second_count = ( int )remaining_time;
            remaining_minute_count = remaining_second_count / 60;
            remaining_second_count -= remaining_minute_count * 60;

            if ( RemainingMinuteCount != remaining_minute_count
                 || RemainingSecondCount != remaining_second_count )
            {
                RemainingMinuteCount = remaining_minute_count;
                RemainingSecondCount = remaining_second_count;

                RemainingTimeText.Clear();
                RemainingTimeText.AddFormattedInteger( RemainingMinuteCount );
                RemainingTimeText.AddCharacter( ':' );
                RemainingTimeText.AddFormattedInteger( RemainingSecondCount, 2 );

                RemainingTimeTextMesh.text = RemainingTimeText.GetString();
            }
        }

        // ~~

        public void UpdateScore(
            )
        {
            int
                score;

            score = LEVEL.Instance.Score;

            if ( Score != score )
            {
                Score = score;

                ScoreText.Clear();
                ScoreText.AddFormattedInteger( Score );

                ScoreTextMesh.text = ScoreText.GetString();
            }
        }

        // ~~

        public void UpdateMinimap(
            )
        {
            float
                enemy_orientation_y_angle,
                hero_orientation_y_angle;
            int
                enemy_index;
            GameObject
                minimap_enemy_game_object;
            Transform
                minimap_enemy_transform;
            Vector3
                enemy_offset_vector,
                enemy_position_vector,
                hero_position_vector;
            ENEMY
                enemy;
            ENEMY[]
                enemy_array;

            hero_position_vector = LEVEL.Instance.Hero.GetPositionVector();
            hero_position_vector.y = 0.0f;
            hero_orientation_y_angle = LEVEL.Instance.Hero.GetOrientationQuaternion().eulerAngles.y;

            MinimapGroundTransform.localPosition = new Vector3( -hero_position_vector.x, -hero_position_vector.z, 0.0f );
            MinimapHeroTransform.localRotation = Quaternion.Euler( 0.0f, 0.0f, -hero_orientation_y_angle );

            enemy_array = LEVEL.Instance.EnemyArray;

            for ( enemy_index = 0;
                  enemy_index < enemy_array.Length;
                  ++enemy_index )
            {
                enemy = enemy_array[ enemy_index ];
                enemy_position_vector = enemy.GetPositionVector();
                enemy_position_vector.y = 0.0f;
                enemy_offset_vector = enemy_position_vector - hero_position_vector;

                minimap_enemy_game_object = MinimapEnemyGameObjectArray[ enemy_index ];

                if ( enemy_offset_vector.sqrMagnitude > MinimapRadius * MinimapRadius
                     || !enemy.IsAlive() )
                {
                    if ( IsMinimapEnemyVisibleArray[ enemy_index ] )
                    {
                        IsMinimapEnemyVisibleArray[ enemy_index ] = false;
                        minimap_enemy_game_object.SetActive( false );
                    }
                }
                else
                {
                    if ( !IsMinimapEnemyVisibleArray[ enemy_index ] )
                    {
                        IsMinimapEnemyVisibleArray[ enemy_index ] = true;
                        minimap_enemy_game_object.SetActive( true );
                    }

                    enemy_orientation_y_angle = enemy.GetOrientationQuaternion().eulerAngles.y;

                    minimap_enemy_transform = minimap_enemy_game_object.transform;

                    minimap_enemy_transform.localPosition = new Vector3( enemy_position_vector.x, enemy_position_vector.z, 0.0f );
                    minimap_enemy_transform.localRotation = Quaternion.Euler( 0.0f, 0.0f, -enemy_orientation_y_angle );
                }
            }
        }

        // ~~

        public void UpdateRemainingEnemyCount(
            )
        {
            int
                enemy_index;

            RemainingEnemyCount = 0;

            for ( enemy_index = 0;
                  enemy_index < EnemyArray.Length;
                  ++enemy_index )
            {
                if ( EnemyArray[ enemy_index ].HasFeatureMask( ( ulong )FEATURE_MASK.Alive ) )
                {
                    ++RemainingEnemyCount;
                }
            }
        }

        // ~~

        public void LateUpdate(
            )
        {
            int
                remaining_second_count;

            BeginUpdate();

            UpdateHealthBar();
            UpdateRemainingTime();
            UpdateScore();
            UpdateMinimap();
            UpdateRemainingEnemyCount();

            remaining_second_count = RemainingMinuteCount * 60 + RemainingSecondCount;

            if ( GAME.Instance.State.HasState( GAME.Instance.PlayLevelState ) )
            {
                if ( Health == 0.0f
                     || remaining_second_count == 0 )
                {
                    GAME.Instance.State.ChangeState( GAME.Instance.FailMissionState );
                }
                else if ( RemainingEnemyCount == 0
                          || Score >= 1000 )
                {
                    GAME.Instance.State.ChangeState( GAME.Instance.CompleteMissionState );
                }
                else if ( INPUT.PauseButtonIsPressed
                          && GAME.Instance.State.HasState( GAME.Instance.PlayLevelState ) )
                {
                    GAME.Instance.State.ChangeState( GAME.Instance.PauseLevelState );
                }
            }

            EndUpdate();
        }

        // ~~

        public void PlayScoreSound(
            )
        {
            GAME.Instance.PlaySoundAudioClip( ScoreAudioClip, SOUND_MASK.Exclusive );
        }
    }
}
