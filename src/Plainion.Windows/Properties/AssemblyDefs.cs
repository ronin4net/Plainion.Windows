﻿using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Markup;

[assembly: XmlnsPrefix( "http://github.com/ronin4net/plainion", "pn" )]
[assembly: XmlnsDefinition( "http://github.com/ronin4net/plainion", "Plainion.Windows" )]
[assembly: XmlnsDefinition( "http://github.com/ronin4net/plainion", "Plainion.Windows.Controls" )]
[assembly: XmlnsDefinition( "http://github.com/ronin4net/plainion", "Plainion.Windows.Controls.Tree" )]
[assembly: XmlnsDefinition( "http://github.com/ronin4net/plainion", "Plainion.Windows.Controls.Text" )]
[assembly: XmlnsDefinition( "http://github.com/ronin4net/plainion", "Plainion.Windows.Interactivity" )]
[assembly: XmlnsDefinition( "http://github.com/ronin4net/plainion", "Plainion.Windows.Interactivity.DragDrop" )]

// sn.exe -Tp <assembly>
[assembly: InternalsVisibleTo("Plainion.Windows.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100656259005e9fc8444ec8025c25d3bbdfb44b2dddd280bcb4fe9f5898d53727b5510c943c68bba6a3ad44014f118b22b0a23b45304773c68870b82ee23677a91574674cb7d73fc2b2cb8dd46f9ec01e4486c3d9ad8134af3bdc1d8e4165b88f226af62f2977ec4735f65a62176ad84b4605a9ab1f0d95050ec1e8f55a5ca513e7")]
[assembly: InternalsVisibleTo("Plainion.Windows.Specs, PublicKey=0024000004800000940000000602000000240000525341310004000001000100656259005e9fc8444ec8025c25d3bbdfb44b2dddd280bcb4fe9f5898d53727b5510c943c68bba6a3ad44014f118b22b0a23b45304773c68870b82ee23677a91574674cb7d73fc2b2cb8dd46f9ec01e4486c3d9ad8134af3bdc1d8e4165b88f226af62f2977ec4735f65a62176ad84b4605a9ab1f0d95050ec1e8f55a5ca513e7")]

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
    //(used if a resource is not found in the page, 
    // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
    //(used if a resource is not found in the page, 
    // app, or any theme specific resource dictionaries)
)]