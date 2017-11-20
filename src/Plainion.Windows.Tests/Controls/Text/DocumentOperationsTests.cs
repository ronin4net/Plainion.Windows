﻿using System.Linq;
using System.Windows.Documents;
using NUnit.Framework;
using Plainion.Windows.Controls.Text;

namespace Plainion.Windows.Tests.Controls.Text.AutoCorrection
{
    [TestFixture]
    class DocumentOperationsTests
    {
        private FlowDocument myDocument;

        [SetUp]
        public void SetUp()
        {
            myDocument = new FlowDocument();
            myDocument.Blocks.Add(new Paragraph(new Run("Here is some text to test parsing")));
            myDocument.Blocks.Add(new Paragraph(new Run("a second line would be helpful.")));
            myDocument.Blocks.Add(new Paragraph(new Run("A third is even better.")));
        }

        [Test]
        public void GetPointerFromCharOffset_BeginningOfRange_ReturnsRangeStart()
        {
            var pos = DocumentOperations.GetPointerFromCharOffset(myDocument.Content(), 0);
            Assert.That(new TextRange(pos, pos.GetPositionAtOffset(5)).Text, Is.EqualTo("Here "));
        }

        [Test]
        public void GetPointerFromCharOffset_AtBeginningOfWord_ReturnsBeginningOfWord()
        {
            var pos = DocumentOperations.GetPointerFromCharOffset(myDocument.Content(), 8);
            Assert.That(new TextRange(pos, pos.GetPositionAtOffset(5)).Text, Is.EqualTo("some "));
        }

        [Test]
        public void GetPointerFromCharOffset_AtEndOfLine_ReturnsEndOfLine()
        {
            var pos = DocumentOperations.GetPointerFromCharOffset(myDocument.Content(), 33);
            Assert.That(new TextRange(pos, pos.GetPositionAtOffset(9)).Text, Is.EqualTo("\r\na sec"));
        }

        [Test]
        public void GetWord_AtBeginningOfWord_ReturnsThatWord()
        {
            var pos = DocumentOperations.GetPointerFromCharOffset(myDocument.Content(), 8);
            Assert.That(DocumentOperations.GetWordAt(pos).Text, Is.EqualTo("some"));
        }

        [Test]
        public void GetWord_InsideWord_ReturnsThatWord()
        {
            var pos = DocumentOperations.GetPointerFromCharOffset(myDocument.Content(), 10);
            Assert.That(DocumentOperations.GetWordAt(pos).Text, Is.EqualTo("some"));
        }

        [Test]
        public void GetWord_EndOfWord_ReturnsThatWord()
        {
            var pos = DocumentOperations.GetPointerFromCharOffset(myDocument.Content(), 12);
            Assert.That(DocumentOperations.GetWordAt(pos).Text, Is.EqualTo("some"));
        }

        [Test]
        public void GetLineAt_WhenCalled_ReturnsLineContent()
        {
            var line = DocumentOperations.GetLineAt(DocumentOperations.GetPointerFromCharOffset(myDocument.Content(), 40));
            Assert.That(line.Text, Is.EqualTo("a second line would be helpful."));
        }

        [Test]
        public void GetWords_WhenCalled_ReturnsAllWordsOfARange()
        {
            var line = DocumentOperations.GetLineAt(DocumentOperations.GetPointerFromCharOffset(myDocument.Content(), 40));
            Assert.That(DocumentOperations.GetWords(line).Select(x => x.Text), Is.EquivalentTo(new[] { "a", "second", "line", "would", "be", "helpful." }));
        }
    }
}
