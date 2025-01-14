using Application.Exceptions;
using Application.Input;
using Application.PointComponents;

namespace Application.Services.AssignStress
{
    /// <inheritdoc cref="IAssignStressService"/>
    public class AssignStressService : IAssignStressService
    {
        /// <inheritdoc />
        public void AssignStress(ICollection<IStress> points, ICollection<Modulus> moduli)
        {
            for (int i = 0; i < points.Count; i++)
            {
                IStress point = points.ElementAt(i);

                Modulus currentModulus = moduli
                    .Where(m => m.From <= point.Position)
                    .MinBy(m => point.Position - m.From)
                    ?? throw new ModulusNotDefinedException();

                Modulus? previousModulus = currentModulus.From == point.Position
                    ? moduli
                        .Where(m => m.From < point.Position)
                        .MinBy(m => point.Position - m.From)
                    : null;

                double stress = point.Moment * 1000 / currentModulus.Value;

                if (point.MomentJump.HasValue)
                {
                    if (previousModulus is not null)
                    {
                        double[] stressRange =
                        [
                            stress,
                            point.MomentJump.Value * 1000 / currentModulus.Value,
                            point.Moment * 1000 / previousModulus.Value,
                            point.MomentJump.Value * 1000 / previousModulus.Value
                        ];
                        if (currentModulus.Value < previousModulus.Value)
                        {
                            point.Stress = stressRange.OrderByDescending(Math.Abs).First();
                            point.StressJump = stressRange.OrderBy(Math.Abs).First();
                        }
                        else
                        {
                            point.Stress = stressRange.OrderBy(Math.Abs).First();
                            point.StressJump = stressRange.OrderByDescending(Math.Abs).First();
                        }
                    }
                    else
                    {
                        point.Stress = stress;
                        point.StressJump = point.MomentJump.Value * 1000 / currentModulus.Value;
                    }
                }
                else
                {
                    point.Stress = stress;

                    if (previousModulus is not null)
                    {
                        point.StressJump = point.Moment * 1000 / previousModulus.Value;
                    }
                }
            }
        }
    }
}
