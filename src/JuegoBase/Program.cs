using Raylib_cs;
using JuegoBase.Source.Const;
using System.Numerics;

namespace JuegoBase
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Raylib.InitWindow(BaseConstants.WINDOW_SCREEN_WIDTH, BaseConstants.WINDOW_SCREEN_HEIGHT, "Hello World");

            Camera3D camera = new Camera3D();
            camera.Position = new Vector3(4.0f, 2.0f, 4.0f);
            camera.Target = new Vector3(0.0f, 1.8f, 0.0f);
            camera.FovY = 60f;
            camera.Projection = CameraProjection.Perspective;
            camera.Up = new Vector3(0f, 1f, 0f);

            while (!Raylib.WindowShouldClose())
            {
                float delta = Raylib.GetFrameTime();

                //Vector3 forward = Vector3.Normalize(camera.Target - camera.Position);

                Vector2 mouse = Raylib.GetMouseDelta();

                float _yaw = mouse.X * BaseConstants.MOUSE_SENSE;
                float _pitch = mouse.Y * BaseConstants.MOUSE_SENSE;

                _pitch = Math.Clamp(_pitch, -1.55f, 1.55f);

                Vector3 forward = new Vector3(
                        MathF.Cos(_pitch) * MathF.Cos(_yaw),
                        MathF.Sin(_pitch),
                        MathF.Cos(_pitch) * MathF.Sin(_yaw));

                Vector3 right = Vector3.Normalize(Vector3.Cross(forward, Vector3.UnitY));

                Vector3 move = Vector3.Zero;

                if (Raylib.IsKeyDown(KeyboardKey.W)) move += forward;
                if (Raylib.IsKeyDown(KeyboardKey.S)) move -= forward;
                if (Raylib.IsKeyDown(KeyboardKey.A)) move -= right;
                if (Raylib.IsKeyDown(KeyboardKey.D)) move += right;

                //if (Raylib.IsKeyDown(KeyboardKey.LeftShift)) _speed = * 2;

                if (move != Vector3.Zero)
                {
                    move = Vector3.Normalize(move) * BaseConstants.CAMERA_SPEED * delta;
                    camera.Position += move;
                    camera.Target = camera.Position + move;
                }

                Raylib.BeginDrawing();

                Raylib.ClearBackground(Color.White);

                Raylib.BeginMode3D(camera);
                
                Raylib.DrawPlane(new Vector3(0.0f, 0.0f, 0.0f), new Vector2(32.0f, 32.0f), Color.LightGray);

                Raylib.EndMode3D();

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}
