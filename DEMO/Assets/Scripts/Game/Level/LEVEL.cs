// -- IMPORTS

using UnityEngine;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    #if UNITY_EDITOR
    [ UPDATE( "GAME", "HERO HERO_CAMERA *" ) ]
    #endif

    public class LEVEL : TEMPORAL_ENTITY
    {
        // -- ATTRIBUTES

        public GRID
            CharacterGrid;
        public HERO_SPAWN
            HeroSpawn;
        public HERO
            Hero;
        public HERO_CAMERA
            HeroCamera;
        public ENEMY_SPAWN[]
            EnemySpawnArray;
        public ENEMY[]
            EnemyArray;
        public int
            Score;
        public float
            RemainingTime;
        public PLAY_LEVEL_PANEL
            PlayLevelPanel;
        public STATE
            StartState,
            PlayState,
            PausedState,
            WonState,
            LostState,
            State;
        public static LEVEL
            Instance;

        // -- OPERATIONS

        public void InstantiateEnemies(
            )
        {
            int
                enemy_spawn_index;
            Vector3
                position_vector;
            Quaternion
                orientation_quaternion;
            GameObject
                footman_prefab_game_object,
                enemy_game_object,
                golem_prefab_game_object,
                grunt_prefab_game_object,
                lich_prefab_game_object,
                prefab_game_object;
            ENEMY_SPAWN
                enemy_spawn;

            footman_prefab_game_object = ( GameObject )Resources.Load( "Prefabs/Footman" );
            golem_prefab_game_object = ( GameObject )Resources.Load( "Prefabs/Golem" );
            grunt_prefab_game_object = ( GameObject )Resources.Load( "Prefabs/Grunt" );
            lich_prefab_game_object = ( GameObject )Resources.Load( "Prefabs/Lich" );
            prefab_game_object = null;

            for ( enemy_spawn_index = 0;
                  enemy_spawn_index < EnemySpawnArray.Length;
                  ++enemy_spawn_index )
            {
                enemy_spawn = EnemySpawnArray[ enemy_spawn_index ];

                position_vector = enemy_spawn.transform.position;
                orientation_quaternion = Quaternion.Euler( 0.0f, 30.0f * enemy_spawn_index, 0.0f );

                if ( enemy_spawn.Type == ENEMY_TYPE.Footman )
                {
                    prefab_game_object = footman_prefab_game_object;
                }
                else if ( enemy_spawn.Type == ENEMY_TYPE.Golem )
                {
                    prefab_game_object = golem_prefab_game_object;
                }
                else if ( enemy_spawn.Type == ENEMY_TYPE.Grunt )
                {
                    prefab_game_object = grunt_prefab_game_object;
                }
                else if ( enemy_spawn.Type == ENEMY_TYPE.Lich )
                {
                    prefab_game_object = lich_prefab_game_object;
                }

                enemy_game_object = Object.Instantiate( prefab_game_object, position_vector, orientation_quaternion );
                enemy_game_object.SetActive( true );
            }
        }

        // ~~

        public void OnEnable(
            )
        {
            Instance = this;

            BLOOD_SPRAY.InitializePool();
            ENERGY_SPRAY.InitializePool();
            HERO_LASER_GUN_RAY.InitializePool();
            LICH_STAFF_FIREBALL.InitializePool();

            CharacterGrid = new GRID( new Vector3( -300.0f, 0.0f, -300.0f ), new Vector3( 300.0f, 100.0f, 300.0f ), 50, 50 );

            HeroSpawn = FindEntityByType<HERO_SPAWN>();
            Hero = FindEntityByType<HERO>();
            Hero.SetPositionVector( HeroSpawn.GetPositionVector() );
            Hero.SetOrientationQuaternion( HeroSpawn.GetOrientationQuaternion() );
            HeroCamera = FindEntityByType<HERO_CAMERA>();
            EnemySpawnArray = FindEntitiesByType<ENEMY_SPAWN>();
            InstantiateEnemies();
            EnemyArray = FindEntitiesByType<ENEMY>();

            Score = 0;
            RemainingTime = 180.0f;

            PlayLevelPanel = FindSubEntityByType<PLAY_LEVEL_PANEL>();

            StartState = new DYNAMIC_STATE( "Start" );
            PlayState = new DYNAMIC_STATE( "Play" );
            PausedState = new DYNAMIC_STATE( "Paused" );
            WonState = new DYNAMIC_STATE( "Won" );
            LostState = new DYNAMIC_STATE( "Lost" );

            State = new STATE( StartState );
        }

        // ~~

        public void Activate(
            )
        {
            int
                enemy_index;

            Hero.IsFrozen = false;

            for ( enemy_index = 0;
                  enemy_index < EnemyArray.Length;
                  ++enemy_index )
            {
                EnemyArray[ enemy_index ].IsFrozen = false;
            }
        }

        // ~~

        public void Update(
            )
        {
            BeginUpdate();
            State.Update();

            if ( GAME.Instance.State.HasState( GAME.Instance.PlayLevelState ) )
            {
                RemainingTime -= TimeStep;
            }

            EndUpdate();
        }

        // ~~

        public void AddScore(
            int added_score
            )
        {
            Score += added_score;

            PlayLevelPanel.PlayScoreSound();
        }
    }
}
