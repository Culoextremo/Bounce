﻿using Bounce.Gameplay.Domain.Runtime;
using FluentAssertions;
using JunityEngine;
using JunityEngine.Maths.Runtime;
using NUnit.Framework;
using static Bounce.Gameplay.Domain.Tests.Builders.AreaBuilder;

namespace Bounce.Gameplay.Domain.Tests.Editor
{
    public class DrawTrampolineTests
    {
        [Test]
        public void BeginDraw()
        {
            var sut = new Sketchbook();

            sut.Draw(Vector2.Zero);

            sut.Drawing.Should().BeTrue();
        }

        [Test]
        public void Draw()
        {
            var sut = new Sketchbook();
            sut.Draw(Vector2.Zero);

            sut.Draw(Vector2.Up);

            sut.Result.Should().BeEquivalentTo(new Trampoline {Origin = Vector2.Zero, End = Vector2.Up});
        }

        [Test]
        public void ClampToMaxLength()
        {
            var sut = new Sketchbook {MaxTrampolineLength = 1};
            sut.Draw(Vector2.Zero);

            sut.Draw(new Vector2(2, 0));

            sut.Result.Should().Be(new Trampoline {Origin = new Vector2(1, 0), End = new Vector2(2, 0)});
        }

        [Test]
        public void EndDraw()
        {
            var sut = new Sketchbook();
            sut.Draw(Vector2.Zero);
            sut.Draw(Vector2.Up);

            sut.StopDrawing();

            sut.Result.Should().BeAssignableTo<INull>();
        }

        [Test]
        public void PlayerDrawsTrampoline()
        {
            var sut = Area().Build();

            sut.Draw(Vector2.Zero);
            sut.Draw(Vector2.Up);

            sut.Trampoline.Should().Be(new Trampoline {Origin = Vector2.Zero, End = Vector2.Up});
        }

        [Test]
        public void TrampolineRemains()
        {
            var sut = Area().Build();
            sut.Draw(Vector2.Zero);
            sut.Draw(Vector2.Up);

            sut.StopDrawing();

            sut.Trampoline.Should().Be(new Trampoline {Origin = Vector2.Zero, End = Vector2.Up});
        }

        [Test]
        public void TrampolineChangesWhenDrawingNew()
        {
            var sut = Area().Build();
            sut.Draw(Vector2.Zero);
            sut.Draw(Vector2.Up);
            sut.StopDrawing();

            sut.Draw(new Vector2(3, 4));

            sut.Trampoline.Should().Be(new Trampoline {Origin = new Vector2(3, 4), End = new Vector2(3, 4)});
        }

        [Test]
        public void MinTrampolineSizeWhenEndingDraw()
        {
            var sut = Area().WithMinTrampolineLength(1).Build();
            sut.Draw(new Vector2(0.25f, 0));
            sut.Draw(new Vector2(0.75f, 0));           
            
            sut.StopDrawing();

            sut.Trampoline.Should().Be(new Trampoline {Origin = Vector2.Zero, End = Vector2.Right});
        }
        
        [Test]
        public void MinTrampolineSizeDoesNotApplyBeforeEndingDraw()
        {
            var sut = Area().WithMinTrampolineLength(1).Build();
            
            sut.Draw(Vector2.Zero);
            sut.Draw(Vector2.HalfRight);
            
            sut.Trampoline.Should().Be(new Trampoline {Origin = Vector2.Zero, End = Vector2.HalfRight});
        }

        [Test]
        public void MinTrampolineSizeWhenOriginIsAtBorder()
        {
            var sut = Area().WithMinTrampolineLength(1).Build();
            sut.Draw(new Vector2(0.25f, 0));
            sut.Draw(new Vector2(0.75f, 0));           
            
            sut.StopDrawing();

            sut.Trampoline.Should().Be(new Trampoline {Origin = Vector2.Zero, End = Vector2.Right});
        }

        [Test]
        public void BeginDrawFromOutsideBoundsDoesNotDraw()
        {
            var sut = Area().WithBounds(Bounds2D.One).Build();

            sut.Draw(new Vector2(2));

            sut.Trampoline.Should().Be(Trampoline.Null);
        }
        
        
        [Test]
        public void DrawingOutsideClamps()
        {
            var sut = Area().WithBounds(Bounds2D.One).Build();
            sut.Draw(Vector2.Zero);

            sut.Draw(new Vector2(2));

            sut.Trampoline.Should().Be(new Trampoline { Origin = Vector2.Zero, End = Vector2.One});
        }
        
        [Test]
        public void DrawingDotDoesNotDraw()
        {
            var sut = Area().WithBounds(Bounds2D.One).Build();
            
            sut.Draw(Vector2.Zero);
            sut.StopDrawing();

            sut.Trampoline.Should().Be(Trampoline.Null);
        }
        
        //Trampolin tamaño minimo borde cercano al origen
        //Trampolin tamaño minimo borde cercano al final.
        //Trampolin tamaño minimo 2 bordes
    }
}
    