using OpenTK.Mathematics;

namespace TextureMapping
{
    public class Camera
    {
        private Vector3 position;
        private Vector3 target;
        private Vector3 up;
        private float aspectRatio;
        private float fov;
        private float nearPlane;
        private float farPlane;

        public Vector3 Position
        {
            get => position;
            set => position = value;
        }

        public Vector3 Target
        {
            get => target;
            set => target = value;
        }

        public float AspectRatio
        {
            get => aspectRatio;
            set => aspectRatio = value;
        }

        public float FieldOfView
        {
            get => fov;
            set => fov = value;
        }

        public Camera(Vector3 position, float aspectRatio)
        {
            this.position = position;
            this.target = Vector3.Zero;
            this.up = Vector3.UnitY;
            this.aspectRatio = aspectRatio;
            this.fov = MathHelper.DegreesToRadians(45.0f);
            this.nearPlane = 0.1f;
            this.farPlane = 100.0f;
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(position, target, up);
        }

        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(fov, aspectRatio, nearPlane, farPlane);
        }

        public void LookAt(Vector3 target)
        {
            this.target = target;
        }

        public void Move(Vector3 offset)
        {
            position += offset;
        }

        public void Rotate(float yaw, float pitch)
        {
            // Simple rotation around the target point
            float radius = (position - target).Length;

            position.X = target.X + radius * MathF.Sin(yaw) * MathF.Cos(pitch);
            position.Y = target.Y + radius * MathF.Sin(pitch);
            position.Z = target.Z + radius * MathF.Cos(yaw) * MathF.Cos(pitch);
        }
    }
}