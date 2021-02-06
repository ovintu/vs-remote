import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
        <div>
            <h1>Interview Sample</h1>
            <p>Provides a way to control and monitor visual studio remotely</p>
            <ul>
                <li>Based on the "ASP.NET Core Web Application / React.js template that comes with visual studio</li>
                <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
                <li><a href='https://facebook.github.io/react/'>React</a> for client-side code</li>
                <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
            </ul>
            <p>Tools/Packages used</p>
            <ul>
                <li>Visual Studio 2019 Community edition</li>
            </ul>
        </div>
    );
  }
}
