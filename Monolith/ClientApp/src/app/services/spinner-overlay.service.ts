import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SpinnerOverlayService {

  public spinnerMessage: string = null;
  public isVisible: boolean = false;

  constructor() { }

  public show(message?: string) {
    if(message){
      this.spinnerMessage = message;
    }
    this.isVisible = true;
  }

  public hide() {
    this.spinnerMessage = null;
    this.isVisible = false;
  }

}
