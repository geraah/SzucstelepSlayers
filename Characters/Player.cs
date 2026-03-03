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

        public float Damage { get; private set; } = 0f;
        public int Lives = 3;

        private PlayerControls controls;
        private StateManager stateManager;
        private List<StaticBody2D> platforms;

        private bool IsGrounded = false;

        private float MoveSpeed = 500f;
        private float JumpForce = -800f;
        private float Gravity = 2000f;

        private float jumpBufferTime = 0.15f;
        private float jumpBufferCounter;

        private float coyoteTime = 0.1f;
        private float coyoteTimeCounter;

        public Vector2 TopLeft => Position - Size / 2;

        public Player(string Name, Vector2 Position, PlayerControls controls, StateManager stateManager, List<StaticBody2D> platforms) {
            
            this.Name = Name;
            this.Position = Position;
            this.Size = new Vector2(60, 80);
            this.controls = controls;
            this.stateManager = stateManager;
            this.platforms = platforms;

        }

        public void TakeDamage(float amount, Vector2 knockback) {

            Damage += amount;
            Velocity += knockback * (1 + Damage * 0.01f);
            
        }

        private void HandleCollisions() {

            IsGrounded = false;

            foreach (StaticBody2D platform in platforms) {

                Vector2 platformTopLeft = platform.Position - platform.Size / 2;
                float platformBottomY = platformTopLeft.Y + platform.Size.Y;
                float platformRightX = platformTopLeft.X + platform.Size.X;

                bool overlapping = (
                    TopLeft.X < platformRightX &&
                    TopLeft.X + Size.X > platformTopLeft.X &&
                    TopLeft.Y < platformBottomY &&
                    TopLeft.Y + Size.Y > platformTopLeft.Y
                );

                if (!overlapping) continue;

                float overlapTop = (TopLeft.Y + Size.Y) - platformTopLeft.Y;
                float overlapBottom = platformBottomY - TopLeft.Y;
                float overlapLeft = (TopLeft.X + Size.X) - platformTopLeft.X;
                float overlapRight = platformRightX - TopLeft.X;

                float minOverlap = Math.Min(Math.Min(overlapTop, overlapBottom), Math.Min(overlapLeft, overlapRight));

                if (minOverlap == overlapTop && Velocity.Y >= 0) {
                    Position.Y = platformTopLeft.Y - Size.Y / 2;
                    Velocity.Y = 0;
                    IsGrounded = true;
                } else if (minOverlap == overlapBottom && Velocity.Y < 0) {
                    Position.Y = platformBottomY + Size.Y / 2;
                    Velocity.Y = 0;
                } else if (minOverlap == overlapLeft) {
                    Position.X = platformTopLeft.X - Size.X / 2;
                } else if (minOverlap == overlapRight) {
                    Position.X = platformRightX + Size.X / 2;
                }

            }
        }
        private void HandleCollisionsX() {

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

                if (Velocity.X > 0) Position.X = platLeft - (Size.X / 2);
                else if (Velocity.X < 0) Position.X = platRight + (Size.X / 2);

                Velocity.X = 0;

            }

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

                } else if (Velocity.Y < 0) {
                    // Felfelé ugrunk, beverjük a fejünket a platform ALJÁBA
                    // (Smash-ben a lebegő platformoknál ezt a blokkot nyugodtan kihagyhatod)
                    Position.Y = platBottom + (Size.Y / 2);
                    Velocity.Y = 0;
                }
            }
            }

        public void Update() {

            float delta = Raylib.GetFrameTime();

            if (IsGrounded) coyoteTimeCounter = coyoteTime;
            else coyoteTimeCounter -= delta;

            if (Raylib.IsKeyPressed(controls.Jump)) jumpBufferCounter = jumpBufferTime;
            else jumpBufferCounter -= delta;

            if (Raylib.IsKeyDown(controls.Left)) Velocity.X = -MoveSpeed;
            else if (Raylib.IsKeyDown(controls.Right)) Velocity.X = MoveSpeed;
            else Velocity.X = 0;

            Position.X += Velocity.X * delta;
            HandleCollisionsX();


            if (IsGrounded && Velocity.Y > 0) Velocity.Y = 0;

            float currentGravity = (Raylib.IsKeyDown(controls.Down)) ? Gravity * 2f : Gravity;
            Velocity.Y += currentGravity * delta;

            Position.Y += Velocity.Y * delta;

            HandleCollisionsY();

            if (jumpBufferCounter > 0 && coyoteTimeCounter > 0) {

                Velocity.Y = JumpForce;

                jumpBufferCounter = 0;
                coyoteTimeCounter = 0;
                IsGrounded = false;

            }
        }

        public void Draw() {

            Raylib.DrawRectangleV(TopLeft, Size, Color.Black);

        }

    }
}
