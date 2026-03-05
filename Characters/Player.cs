using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;

namespace SzűcstelepSlayers {
    public class Player : IGameObject, IDamageable {

        public string Name;

        public Vector2 Position;
        public Vector2 Size;
        public Vector2 Velocity;

        public Rectangle Hitbox => new Rectangle(TopLeft.X, TopLeft.Y, Size.X, Size.Y);

        public float Damage { get; private set; } = 0f;
        public int Lives = 3;

        private Playing playingScene;
        private PlayerControls controls;
        private StateManager stateManager;
        private List<StaticBody2D> platforms;

        private bool IsGrounded = false;

        private float MoveSpeed = 500f;
        private float JumpForce = -800f;
        private float Gravity = 2000f;

        private float JumpCutMultiplier = 0.5f;

        private float jumpBufferTime = 0.15f;
        private float jumpBufferCounter;

        private float coyoteTime = 0.1f;
        private float coyoteTimeCounter;

        private float maxWallJumpLockTime = 0.2f;
        private float wallJumpLockCounter;

        private int maxJumps = 2;
        private int jumpsRemaining;
        private float AirJumpMultiplier = 0.9f;

        private int wallDirection;
        private float WallSlideSpeed = 150f;
        private bool isWallSliding;

        private bool canDash = true;
        private bool isDashing = false;
        private float dashPower = 1200f;
        private float dashTime = 0.15f;
        private float dashCooldown = 0.5f;
        private float dashTimer;
        private float dashCooldownTimer;

        private int facingDirection = 1;

        private float SuperDashMultiplierX = 1f;
        private float SuperDashMultiplierY = 1f;

        private bool isAttacking = false;
        private float attackDuration = 0.15f;
        private float attackTimer;
        private float attackCooldown = 0.4f;
        private float attackCooldownTimer;

        private float attackDamage = 10f;
        private float attackKnockbackForce = 400f;
        private Vector2 attackHitboxSize = new Vector2(100, 60);

        private float knockbackLockTime = 0.3f;
        private float knockbackLockCounter;
        private float knockbackMultiplier = 0.02f;

        public Vector2 TopLeft => Position - Size / 2;

        public Player(string Name, Vector2 Position, PlayerControls controls, StateManager stateManager, List<StaticBody2D> platforms, Playing playingScene) {
            
            this.Name = Name;
            this.Position = Position;
            this.Size = new Vector2(60, 80);
            this.controls = controls;
            this.stateManager = stateManager;
            this.platforms = platforms;
            this.playingScene = playingScene;

        }

        public void TakeDamage(float amount, Vector2 knockback) {

            Damage += amount;
            Velocity += knockback * (1 + Damage * knockbackMultiplier);

            knockbackLockCounter = knockbackLockTime;

        }

        public void Respawn(Vector2 spawnPosition) {

            Position = spawnPosition;
            Velocity = Vector2.Zero;
            Damage = 0f;
            Lives--;

            knockbackLockCounter = 0;
            wallJumpLockCounter = 0;
            isDashing = false;
            isAttacking = false;

        }

        private void HandleCollisionsX() {

            wallDirection = 0;

            foreach (StaticBody2D platform in platforms) {

                float myLeft = TopLeft.X;
                float myRight = TopLeft.X + Size.X;
                float myTop = TopLeft.Y;
                float myBottom = TopLeft.Y + Size.Y;

                float platLeft = platform.TopLeft.X;
                float platRight = platform.TopLeft.X + platform.Size.X;
                float platTop = platform.TopLeft.Y;
                float platBottom = platform.TopLeft.Y + platform.Size.Y;

                bool overlapping = myRight > platLeft &&
                                   myLeft < platRight &&
                                   myBottom > platTop &&
                                   myTop < platBottom;

                if (!overlapping) continue;

                if (Velocity.X > 0) {

                    Position.X = platLeft - (Size.X / 2);
                    wallDirection = 1;

                } else if (Velocity.X < 0) {
                    
                    Position.X = platRight + (Size.X / 2);
                    wallDirection = -1;
                    
                }
                
                Velocity.X = 0;

            }

        }

        private void ReplenishJumps() {

            jumpsRemaining = maxJumps;

        }

        private void SubtractJumps() {

            jumpsRemaining--;

        }

        private void ReplenishDash() {

            canDash = true;

        }

        private void HandleCollisionsY() {
            
            IsGrounded = false;

            foreach (StaticBody2D platform in platforms) {

                float myLeft = TopLeft.X;
                float myRight = TopLeft.X + Size.X;
                float myTop = TopLeft.Y;
                float myBottom = TopLeft.Y + Size.Y;

                float platLeft = platform.TopLeft.X;
                float platRight = platform.TopLeft.X + platform.Size.X;
                float platTop = platform.TopLeft.Y;
                float platBottom = platform.TopLeft.Y + platform.Size.Y;

                bool overlapping = myRight > platLeft &&
                                   myLeft < platRight &&
                                   myBottom > platTop &&
                                   myTop < platBottom;

                if (!overlapping) continue;

                if (Velocity.Y > 0) {

                    Position.Y = platTop - (Size.Y / 2);
                    Velocity.Y = 0;
                    IsGrounded = true;

                    ReplenishJumps();
                    ReplenishDash();

                } else if (Velocity.Y < 0) {
                    Position.Y = platBottom + (Size.Y / 2);
                    Velocity.Y = 0;
                }
            }
            }

        private void HandleWallSlide() {

            isWallSliding = false;

            if (!IsGrounded && wallDirection != 0) {

                bool pushingWall = (wallDirection == 1 && Raylib.IsKeyDown(controls.Right)) ||
                                   (wallDirection == -1 && Raylib.IsKeyDown(controls.Left));

                if (pushingWall && Velocity.Y > 0) {
                    
                    isWallSliding = true;
                    
                    if (Velocity.Y > WallSlideSpeed) {
                        Velocity.Y = WallSlideSpeed;
                    }

                }

            }

        }
        
        public void ApplyMovement(float delta) {

            Position.X += Velocity.X * delta;
            HandleCollisionsX();
            Position.Y += Velocity.Y * delta;
            HandleCollisionsY();

        }

        private Rectangle GetAttackHitbox() {

            float hitboxX = (facingDirection == 1)
                ? Position.X + (Size.X / 2)
                : Position.X - (Size.X / 2) - attackHitboxSize.X;

            float hitboxY = Position.Y - (attackHitboxSize.Y / 2);

            return new Rectangle(hitboxX, hitboxY, attackHitboxSize.X, attackHitboxSize.Y);

        }

        private void PerformAttack() {

            Rectangle attackHitbox = GetAttackHitbox();
            Vector2 knockbackVector = new Vector2(facingDirection, -0.5f) * attackKnockbackForce;

            List<IDamageable> targets = playingScene.GetDamageables();

            foreach (IDamageable target in targets) {

                if (target == this) continue;

                if (Raylib.CheckCollisionRecs(attackHitbox, target.Hitbox)) target.TakeDamage(attackDamage, knockbackVector);

            }

        }

        public void Update() {

            float delta = Raylib.GetFrameTime();

            if (Raylib.IsKeyDown(controls.Left)) facingDirection = -1;
            else if (Raylib.IsKeyDown(controls.Right)) facingDirection = 1;

            if (dashCooldownTimer > 0) dashCooldownTimer -= delta;
            if (attackCooldownTimer > 0) attackCooldownTimer -= delta;
            if (knockbackLockCounter > 0) knockbackLockCounter -= delta;

            if (isAttacking) {

                attackTimer -= delta;
                if (attackTimer <= 0) isAttacking = false;

            }

            if (Raylib.IsKeyPressed(controls.Attack) && attackCooldownTimer <= 0) {

                isAttacking = true;
                attackTimer = attackDuration;
                attackCooldownTimer = attackCooldown;

                PerformAttack();

            }

            if (isDashing && Raylib.IsKeyPressed(controls.Jump)) {

                isDashing = false;

                Velocity.Y = JumpForce * SuperDashMultiplierY;
                Velocity.X = facingDirection * dashPower * SuperDashMultiplierX;

                wallJumpLockCounter = 0.2f;

                jumpBufferCounter = 0;

            }

            if (Raylib.IsKeyPressed(controls.Dash) && canDash && dashCooldownTimer <= 0) {

                isDashing = true;
                canDash = false;
                dashTimer = dashTime;
                dashCooldownTimer = dashCooldown;

                Velocity.X = facingDirection * dashPower;
                Velocity.Y = 0;

            }

            if (isDashing) {

                dashTimer -= delta;

                if (dashTimer <= 0) {

                    isDashing = false;
                    Velocity.X *= 0.5f;

                }

                ApplyMovement(delta);
                return;

            }

            if (IsGrounded) {

                coyoteTimeCounter = coyoteTime;
                ReplenishJumps();

            } else coyoteTimeCounter -= delta;

            if (Raylib.IsKeyPressed(controls.Jump)) jumpBufferCounter = jumpBufferTime;
            else jumpBufferCounter -= delta;

            if (wallJumpLockCounter > 0) {

                wallJumpLockCounter -= delta;

                if (!Raylib.IsKeyDown(controls.Jump)) wallJumpLockCounter = 0;

            }

            if (wallJumpLockCounter <= 0 && knockbackLockCounter <= 0) {

                if (Raylib.IsKeyDown(controls.Left)) Velocity.X = -MoveSpeed;
                else if (Raylib.IsKeyDown(controls.Right)) Velocity.X = MoveSpeed;
                else Velocity.X = 0;

            }
            
            if (IsGrounded && Velocity.Y > 0) Velocity.Y = 0;

            if (!isDashing) {
                
                float currentGravity = (Raylib.IsKeyDown(controls.Down)) ? Gravity * 2f : Gravity;
                Velocity.Y += currentGravity * delta;

            }

            if (Raylib.IsKeyReleased(controls.Jump) && Velocity.Y < 0) Velocity.Y *= JumpCutMultiplier;

            ApplyMovement(delta);

            HandleWallSlide();

            if (jumpBufferCounter > 0 && coyoteTimeCounter > 0) {

                Velocity.Y = JumpForce;

                SubtractJumps();

                jumpBufferCounter = 0;
                coyoteTimeCounter = 0;

                IsGrounded = false;

            } else if (jumpBufferCounter > 0 && isWallSliding) {

                Velocity.Y = JumpForce;

                Velocity.X = -wallDirection * MoveSpeed;

                wallJumpLockCounter = maxWallJumpLockTime;

                jumpsRemaining = maxJumps - 1;
                jumpBufferCounter = 0;
                isWallSliding = false;

            } else if (Raylib.IsKeyPressed(controls.Jump) && jumpsRemaining > 0 && !IsGrounded) {

                Velocity.Y = JumpForce * AirJumpMultiplier;
                SubtractJumps();
                jumpBufferCounter = 0;

            }
        }

        public void Draw() {

            Raylib.DrawRectangleV(TopLeft, Size, Color.Black);

            float dotX = (facingDirection == 1) ? TopLeft.X + Size.X - 10 : TopLeft.X + 5;
            Raylib.DrawRectangle((int) dotX, (int)TopLeft.Y + 10, 5, 5, Color.White);
            
            if (isAttacking) {

                Rectangle hitbox = GetAttackHitbox();
                Color hitboxColor = Color.Black;

                Raylib.DrawRectangleRec(hitbox, hitboxColor);

                hitbox.Size *= 0.8f;
                hitboxColor = Color.White;

                Raylib.DrawRectangleRec(hitbox, hitboxColor);

            }


        }

    }
}
