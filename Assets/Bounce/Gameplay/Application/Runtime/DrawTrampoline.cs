using Bounce.Gameplay.Domain.Runtime;
using Bounce.Gameplay.Input.Runtime;
using UnityEngine;
using Vector2 = JunityEngine.Maths.Runtime.Vector2;

namespace Bounce.Gameplay.Application.Runtime
{
    public class  DrawTrampoline 
    {
        readonly Game game;
        readonly Player player;
        readonly DrawTrampolineView drawTrampolineView;
        readonly TrampolinesView trampolinesView;
        readonly DrawTrampolineInput drawingInput;
        public DrawTrampoline(Game game, Player player, DrawTrampolineView drawTrampolineView, TrampolinesView trampolinesView, DrawTrampolineInput drawingInput)
        {
            this.game = game;
            this.player = player;
            this.drawTrampolineView = drawTrampolineView;
            this.trampolinesView = trampolinesView;
            this.drawingInput = drawingInput;
        }

        public void EnableDraw()
        {
            drawingInput.DrawInputReceived += Draw;
            drawingInput.EndDrawInputReceived += EndDraw;
        }
        
        public void DisableDraw()
        {
            drawingInput.DrawInputReceived -= Draw;
            drawingInput.EndDrawInputReceived -= EndDraw;
        }

        void Draw(Vector2 position)
        {
            trampolinesView.RemoveCurrent(player);
            game.Draw(player, position);
            drawTrampolineView.Draw(game.TrampolineOf(player));
        }

        void EndDraw()
        {
            if (!game.IsDrawing(player))
                return;
            
            game.StopDrawing(player);
            if(game.TrampolineOf(player) != Trampoline.Null)
            {
                trampolinesView.Add(player, game.TrampolineOf(player));
            }
            drawTrampolineView.StopDrawing(game.TrampolineOf(player));
        }
    }
}
