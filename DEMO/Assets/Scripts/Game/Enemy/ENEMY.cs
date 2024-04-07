// -- IMPORTS

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CORE_MODULE;

// -- TYPES

namespace GAME_MODULE
{
    #if UNITY_EDITOR
    [ UPDATE( "HERO", "*" ) ]
    #endif

    public class ENEMY : CHARACTER
    {
        // -- ATTRIBUTES

        public ENEMY_INPUT
            Input;
        public int
            KillScore;
        public float
            SideDistanceFactor,
            MinimumAttackDistance,
            MaximumAttackDistance,
            MaximumAttackAngle,
            MumbleDelay,
            AttackDelay;
        public bool
            HasBeenHit;
        public Vector3
            NavigationTranslationVelocityVector;
        public NavMeshAgent
            NavigationMeshAgent;
        public Vector3
            DestinationPositionVector;
        public AudioClip
            MumbleAudioClip,
            AttackAudioClip,
            DieAudioClip;
        public AnimationClip
            StandAnimationClip,
            WalkForwardAnimationClip,
            RunForwardAnimationClip,
            AttackAnimationClip,
            SufferAnimationClip,
            DieAnimationClip;
        public ANIMATION
            StandAnimation,
            WalkForwardAnimation,
            RunForwardAnimation,
            AttackAnimation,
            SufferAnimation,
            DieAnimation;
        public ANIMATION_MIXER
            AnimationMixer,
            WalkAnimationMixer;
        public ANIMATION_CHANNEL
            WalkAnimationChannel,
            StandAnimationChannel,
            WalkForwardAnimationChannel,
            RunForwardAnimationChannel,
            AttackAnimationChannel,
            SufferAnimationChannel,
            DieAnimationChannel;
        public ENEMY_WALK_ANIMATION_STATE
            WalkAnimationState;
        public ENEMY_ATTACK_ANIMATION_STATE
            AttackAnimationState;
        public ENEMY_SUFFER_ANIMATION_STATE
            SufferAnimationState;
        public ENEMY_DIE_ANIMATION_STATE
            DieAnimationState;
        public STATE
            AnimationState;
        public ENEMY_PATROL_STATE
            PatrolState;
        public ENEMY_CHASE_STATE
            ChaseState;
        public ENEMY_ATTACK_STATE
            AttackState;
        public ENEMY_DIE_STATE
            DieState;
        public STATE
            State;

        // -- INQUIRIES

        public bool IsEmergedPosition(
            Vector3 position_vector
            )
        {
            return GetTerrainHeight( position_vector ) > EmergedHeight;
        }

        // ~~

        public bool SeesHero(
            float maximum_distance,
            float maximum_angle
            )
        {
            return LooksEntity( LEVEL.Instance.Hero, maximum_distance, maximum_angle );
        }

        // ~~

        public bool IsNearHero(
            float maximum_distance
            )
        {
            return IsNearEntity( LEVEL.Instance.Hero, maximum_distance );
        }

        // ~~

        public bool SeesChasingEnemy(
            float maximum_distance,
            float maximum_angle
            )
        {
            return
                LEVEL.Instance.CharacterGrid.FindVisibleEntity(
                    this,
                    maximum_distance,
                    maximum_angle,
                    ( ulong )( FEATURE_MASK.Alive | FEATURE_MASK.Chasing | FEATURE_MASK.Enemy )
                    ) != null;
        }

        // ~~

        public bool IsNearChasingEnemy(
            float maximum_distance
            )
        {

            return
                LEVEL.Instance.CharacterGrid.FindNearEntity(
                    this,
                    maximum_distance,
                    ( ulong )( FEATURE_MASK.Alive | FEATURE_MASK.Chasing | FEATURE_MASK.Enemy )
                    ) != null;
        }

        // ~~

        public virtual bool CanAttackHero(
            )
        {
            return
                LEVEL.Instance.Hero.IsAlive()
                && SeesHero( MaximumAttackDistance, MaximumAttackAngle );
        }

        // -- OPERATIONS

        public new void OnEnable(
            )
        {
            base.OnEnable();

            AddFeatureMask( ( ulong )FEATURE_MASK.Enemy );

            MumbleDelay = Random.Range( 5.0f, 15.0f );

            NavigationMeshAgent = GetComponent<NavMeshAgent>();
            NavigationMeshAgent.angularSpeed = MaximumWalkYRotationSpeed;
            NavigationMeshAgent.updatePosition = false;
            NavigationMeshAgent.updateRotation = false;

            StandAnimation = CreateAnimation( "StandAnimation", StandAnimationClip );
            WalkForwardAnimation = CreateAnimation( "WalkForwardAnimation", WalkForwardAnimationClip );
            WalkForwardAnimation.TranslationVelocityVector.z = WalkForwardAnimationSpeed;
            RunForwardAnimation = CreateAnimation( "RunForwardAnimation", RunForwardAnimationClip );
            RunForwardAnimation.TranslationVelocityVector.z = RunForwardAnimationSpeed;
            AttackAnimation = CreateAnimation( "AttackAnimation", AttackAnimationClip );
            SufferAnimation = CreateAnimation( "SufferAnimation", SufferAnimationClip );
            DieAnimation = CreateAnimation( "DieAnimation", DieAnimationClip );

            WalkAnimationMixer = CreateAnimationMixer( "WalkAnimationMixer", 3 );

            StandAnimationChannel = WalkAnimationMixer.CreateChannel( StandAnimation );
            WalkForwardAnimationChannel = WalkAnimationMixer.CreateChannel( WalkForwardAnimation );
            RunForwardAnimationChannel = WalkAnimationMixer.CreateChannel( RunForwardAnimation );

            AnimationMixer = CreateAnimationMixer( "AnimationMixer", 4 );

            WalkAnimationChannel = AnimationMixer.CreateChannel( WalkAnimationMixer );
            AttackAnimationChannel = AnimationMixer.CreateChannel( AttackAnimation );
            SufferAnimationChannel = AnimationMixer.CreateChannel( SufferAnimation );
            DieAnimationChannel = AnimationMixer.CreateChannel( DieAnimation );

            SetAnimation( AnimationMixer );

            WalkAnimationState = new ENEMY_WALK_ANIMATION_STATE( "WalkAnimationState", this );
            AttackAnimationState = new ENEMY_ATTACK_ANIMATION_STATE( "AttackAnimationState", this );
            SufferAnimationState = new ENEMY_SUFFER_ANIMATION_STATE( "SufferAnimationState", this );
            DieAnimationState = new ENEMY_DIE_ANIMATION_STATE( "DieAnimationState", this );

            AnimationState = new STATE( WalkAnimationState );

            PatrolState = new ENEMY_PATROL_STATE( "PatrolState", this );
            ChaseState = new ENEMY_CHASE_STATE( "ChaseState", this );
            AttackState = new ENEMY_ATTACK_STATE( "AttackState", this );
            DieState = new ENEMY_DIE_STATE( "DieState", this );

            State = new STATE( PatrolState );
        }

        // ~~

        public override bool AddDamage(
            int damage
            )
        {
            base.AddDamage( damage );

            HasBeenHit = true;

            return true;
        }

        // ~~

        public void AlertPatrollingEnemies(
            float maximum_distance
            )
        {
            int
                grid_reference_index;
            List<GRID_REFERENCE>
                grid_reference_list;
            ENEMY
                enemy;

            grid_reference_list
                = LEVEL.Instance.CharacterGrid.FindNearEntities(
                      this,
                      maximum_distance,
                      ( ulong )( FEATURE_MASK.Alive | FEATURE_MASK.Patrolling | FEATURE_MASK.Enemy )
                      );

            if ( grid_reference_list != null )
            {
                for ( grid_reference_index = 0;
                      grid_reference_index < grid_reference_list.Count;
                      ++grid_reference_index )
                {
                    enemy = ( ENEMY )( grid_reference_list[ grid_reference_index ].Entity );

                    if ( enemy.State.HasState( enemy.PatrolState ) )
                    {
                        enemy.State.ChangeState( enemy.ChaseState );
                    }
                }
            }
        }

        // ~~

        public override void Die(
            )
        {
            base.Die();

            State.ChangeState( DieState );
        }

        // -- STATES

        public void EnterWalkAnimationState(
            STATE old_state,
            STATE new_state
            )
        {
            AnimationMixer.ClearWeights( 10.0f );
            WalkAnimationChannel.SetWeight( 1.0f );

            if ( WalkAnimationChannel.GetOldWeight() == 0.0f )
            {
                WalkAnimationChannel.SetTime( 0.0f );
            }
        }

        // ~~

        public void UpdateNavigationInput(
            )
        {
            float
                y_orientation_angle,
                y_rotation_angle;
            Quaternion
                orientation_quaternion,
                inverse_orientation_quaternion;
            Vector3
                local_velocity_vector,
                destination_axis_vector;

            NavigationTranslationVelocityVector
                = Vector3.Lerp( NavigationTranslationVelocityVector, NavigationMeshAgent.desiredVelocity, TimeStep * 1.5f );

            NavigationTranslationVelocityVector.y = 0.0f;

            if ( NavigationTranslationVelocityVector.sqrMagnitude > 0.001f * 0.001f )
            {
                destination_axis_vector = NavigationTranslationVelocityVector.normalized;

                y_orientation_angle = Vector3.SignedAngle( Vector3.forward, destination_axis_vector, Vector3.up );
                orientation_quaternion = Quaternion.Euler( 0.0f, y_orientation_angle, 0.0f );

                y_rotation_angle = Vector3.SignedAngle( OrientationQuaternion * Vector3.forward, destination_axis_vector, Vector3.up );
                Input.WalkYRotationSpeed = y_rotation_angle * 0.2f / TimeStep;
            }
            else
            {
                orientation_quaternion = OrientationQuaternion;
                Input.WalkYRotationSpeed = 0.0f;
            }

            inverse_orientation_quaternion = Quaternion.Inverse( orientation_quaternion );
            local_velocity_vector = inverse_orientation_quaternion * NavigationTranslationVelocityVector;

            Input.WalkXTranslationSpeed = local_velocity_vector.x;
            Input.WalkZTranslationSpeed = local_velocity_vector.z;
        }

        // ~~

        public void UpdateWalkAnimation(
            )
        {
            float
                walk_forward_animation_speed;

            WalkAnimationMixer.AddedRotationVelocityVector.Set( 0.0f, Input.WalkYRotationSpeed, 0.0f );
            WalkAnimationMixer.ClearWeights();

            walk_forward_animation_speed = Input.WalkZTranslationSpeed / WalkForwardAnimationSpeed;

            if ( walk_forward_animation_speed < 0.0f )
            {
                WalkForwardAnimationChannel.SetWishedSpeed( -walk_forward_animation_speed, 0.2f, -1.0f );
            }
            else
            {
                WalkForwardAnimationChannel.SetWishedSpeed( walk_forward_animation_speed, 0.2f );
            }

            WalkAnimationMixer.NormalizeWeights();
        }

        // ~~

        public void UpdateAttackAnimation(
            )
        {
            float
                y_rotation_angle,
                y_rotation_speed;
            Vector3
                hero_axis_vector,
                hero_translation_vector;

            hero_translation_vector = ( LEVEL.Instance.Hero.GetPositionVector() - GetPositionVector() );
            hero_translation_vector.y = 0.0f;
            hero_axis_vector = hero_translation_vector.normalized;

            y_rotation_angle = Vector3.SignedAngle( OrientationQuaternion * Vector3.forward, hero_axis_vector, Vector3.up );
            y_rotation_speed = y_rotation_angle / TimeStep;

            if ( y_rotation_speed < -MaximumWalkYRotationSpeed )
            {
                y_rotation_speed = -MaximumWalkYRotationSpeed;
            }
            else if ( y_rotation_speed > MaximumWalkYRotationSpeed )
            {
                y_rotation_speed = MaximumWalkYRotationSpeed;
            }

            AnimationMixer.AddedRotationVelocityVector.Set( 0.0f, y_rotation_speed, 0.0f );
        }

        // ~~

        public bool FindPatrolDestination(
            )
        {
            float
                maximum_angle;

            maximum_angle = Random.Range( 30.0f, 90.0f );

            DestinationPositionVector
                = PositionVector
                  + Quaternion.AngleAxis( Random.Range( -maximum_angle, maximum_angle ), Vector3.up )
                    * ( transform.forward * Random.Range( 1.0f, 32.0f ) );

            return IsEmergedPosition( DestinationPositionVector );
        }

        // ~~

        public void UpdatePatrolDestination(
            )
        {
            PatrolState.Time -= TimeStep;

            if ( !NavigationMeshAgent.pathPending
                 && ( !NavigationMeshAgent.hasPath
                      || NavigationMeshAgent.remainingDistance < NavigationMeshAgent.radius
                      || PatrolState.Time <= 0.0f )
                 && FindPatrolDestination() )
            {
                NavigationMeshAgent.speed = WalkForwardAnimationSpeed;
                NavigationMeshAgent.destination = DestinationPositionVector;
                PatrolState.Time = Random.Range( 8.0f, 16.0f );
            }

            NavigationMeshAgent.nextPosition = PositionVector;
        }

        // ~~

        public bool FindChaseDestination(
            )
        {
            float
                hero_distance,
                side_distance;
            Vector3
                hero_position_vector,
                hero_translation_vector,
                side_axis_vector;

            hero_position_vector = LEVEL.Instance.Hero.GetPositionVector();
            hero_translation_vector = ( hero_position_vector - GetPositionVector() );
            hero_distance = hero_translation_vector.magnitude;

            side_axis_vector = new Vector3( hero_translation_vector.z, 0.0f,  hero_translation_vector.x ).normalized;
            side_distance = hero_distance * SideDistanceFactor;

            DestinationPositionVector
                = hero_position_vector
                  + side_axis_vector * Random.Range( -side_distance, side_distance );

            return IsEmergedPosition( DestinationPositionVector );
        }

        // ~~

        public void UpdateChaseDestination(
            )
        {
            ChaseState.Time -= TimeStep;

            if ( !NavigationMeshAgent.pathPending
                 && ( !NavigationMeshAgent.hasPath
                      || NavigationMeshAgent.remainingDistance < NavigationMeshAgent.radius
                      || ChaseState.Time <= 0.0f )
                 && FindChaseDestination() )
            {
                NavigationMeshAgent.speed = WalkForwardAnimationSpeed;
                NavigationMeshAgent.destination = DestinationPositionVector;
                ChaseState.Time = Random.Range( 0.5f, 1.0f );
            }

            NavigationMeshAgent.nextPosition = PositionVector;
        }

        // ~~

        public void Mumble(
            )
        {
            MumbleDelay -= TimeStep;

            if ( MumbleDelay <= 0.0f )
            {
                MumbleDelay = Random.Range( 5.0f, 15.0f );

                SOUND.Create( GetPositionVector(), MumbleAudioClip, SOUND_MASK.Disposable, 0, Random.Range( 0.025f, 0.1f ), Random.Range( 0.8f, 1.2f ) );
            }
        }

        // ~~

        public void UpdateWalkAnimationState(
            )
        {
        }

        // ~~

        public void EnterAttackAnimationState(
            STATE old_state,
            STATE new_state
            )
        {
            AnimationMixer.ClearWeights( 10.0f );
            AttackAnimationChannel.SetWeight( 1.0f );
            AttackAnimationChannel.SetTime( 0.0f );
        }

        // ~~

        public void UpdateAttackAnimationState(
            )
        {
        }

        // ~~

        public void EnterSufferAnimationState(
            STATE old_state,
            STATE new_state
            )
        {
            AnimationMixer.ClearWeights( 10.0f );
            SufferAnimationChannel.SetWeight( 1.0f );
            SufferAnimationChannel.SetTime( 0.0f );
        }

        // ~~

        public void UpdateSufferAnimationState(
            )
        {
        }

        // ~~

        public void EnterDieAnimationState(
            STATE old_state,
            STATE new_state
            )
        {
            AnimationMixer.ClearWeights( 10.0f );
            DieAnimationChannel.SetWeight( 1.0f );
            DieAnimationChannel.SetTime( 0.0f );

            LEVEL.Instance.AddScore( KillScore );
        }

        // ~~

        public void UpdateDieAnimationState(
            )
        {
        }

        // ~~

        public void EnterPatrolState(
            STATE old_state,
            STATE new_state
            )
        {
            AddFeatureMask( ( ulong )FEATURE_MASK.Patrolling );

            PatrolState.Time = 0.0f;
            AnimationState.ChangeState( WalkAnimationState );

            NavigationMeshAgent.stoppingDistance = 0.5f;
        }

        // ~~

        public void UpdatePatrolState(
            )
        {
            Mumble();

            UpdateNavigationInput();

            UpdateWalkAnimation();
            UpdatePatrolDestination();

            if ( LEVEL.Instance.Hero.IsAlive() )
            {
                if ( HasBeenHit
                     || SeesHero( 50.0f, 120.0f )
                     || IsNearHero( 5.0f )
                     || SeesChasingEnemy( 20.0f, 120.0f )
                     || IsNearChasingEnemy( 10.0f ) )
                {
                    State.ChangeState( ChaseState );
                }
            }
        }

        // ~~

        public void ExitPatrolState(
            STATE old_state,
            STATE new_state
            )
        {
            RemoveFeatureMask( ( ulong )FEATURE_MASK.Patrolling );
        }

        // ~~

        public void EnterChaseState(
            STATE old_state,
            STATE new_state
            )
        {
            AddFeatureMask( ( ulong )FEATURE_MASK.Chasing );

            ChaseState.Time = 0.0f;
            AnimationState.ChangeState( WalkAnimationState );

            NavigationMeshAgent.stoppingDistance = MinimumAttackDistance * 0.5f;
        }

        // ~~

        public void UpdateChaseState(
            )
        {
            Mumble();

            UpdateNavigationInput();

            UpdateWalkAnimation();
            UpdateChaseDestination();

            AttackDelay -= TimeStep;

            if ( LEVEL.Instance.Hero.IsAlive() )
            {
                if ( CanAttackHero()
                     && AttackDelay <= 0.0f )
                {
                    State.ChangeState( AttackState );
                }
            }
            else
            {
                State.ChangeState( PatrolState );
            }
        }

        // ~~

        public void ExitChaseState(
            STATE old_state,
            STATE new_state
            )
        {
            RemoveFeatureMask( ( ulong )FEATURE_MASK.Chasing );
        }

        // ~~

        public virtual void EnterAttackState(
            STATE old_state,
            STATE new_state
            )
        {
            AttackState.Time = 0.0f;
            AnimationState.ChangeState( AttackAnimationState );
            NavigationMeshAgent.ResetPath();

            SOUND.Create( GetPositionVector(), AttackAudioClip, SOUND_MASK.Disposable, 0, 0.1f, Random.Range( 0.8f, 1.2f ) );
        }

        // ~~

        public virtual void UpdateAttackState(
            )
        {
            AttackState.Time += TimeStep;

            if ( AttackState.Time < AttackAnimationClip.length * 0.4f )
            {
                UpdateAttackAnimation();
            }

            if ( AttackState.Time >= AttackAnimationClip.length )
            {
                AttackDelay = Random.Range( 0.0f, 0.5f );

                State.ChangeState( ChaseState );
            }
        }

        // ~~

        public virtual void ExitAttackState(
            STATE old_state,
            STATE new_state
            )
        {
        }

        // ~~

        public void EnterDieState(
            STATE old_state,
            STATE new_state
            )
        {
            AlertPatrollingEnemies( 50.0f );

            DieState.Time = 0.0f;
            AnimationState.ChangeState( DieAnimationState );
            Controller.enabled = false;
            NavigationMeshAgent.ResetPath();

            SOUND.Create( GetPositionVector(), DieAudioClip, SOUND_MASK.Disposable, 0, 0.2f, Random.Range( 0.8f, 1.2f ) );
        }

        // ~~

        public void UpdateDieState(
            )
        {
            DieState.Time += TimeStep;
        }

        // ~~

        public void Update(
            )
        {
            BeginUpdate();

            AnimationState.Update();

            State.BeginUpdate();
            State.Update();
            State.EndUpdate();

            EndUpdate();

            AnimationMixer.AddedTranslationVelocityVector.Set( 0.0f, 0.0f, 0.0f );
            AnimationMixer.AddedRotationVelocityVector.Set( 0.0f, 0.0f, 0.0f );
        }
    }
}
