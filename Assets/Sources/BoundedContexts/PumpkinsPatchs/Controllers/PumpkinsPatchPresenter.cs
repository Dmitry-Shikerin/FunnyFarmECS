using Sources.BoundedContexts.PumpkinsPatchs.Presentation;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.MVPPassiveView.Controllers.Implementation;

namespace Sources.BoundedContexts.PumpkinsPatchs.Controllers
{
    public class PumpkinsPatchPresenter : PresenterBase
    {
        public PumpkinsPatchPresenter(
            string id, 
            PumpkinsPatchView view, 
            IEntityRepository entityRepository)
        {
        }
    }
}