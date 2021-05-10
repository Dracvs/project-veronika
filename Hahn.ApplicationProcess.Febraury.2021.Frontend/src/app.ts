import { bindable } from 'aurelia-framework';
import 'bootstrap/dist/css/bootstrap.css';
export class App {
  public heading: string;
  public message: string;
  
  constructor(){
    this.heading = "The Heading";  
    this.message = 'Hello World!';
  }
}
