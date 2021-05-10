import { Asset } from './../model/asset';
import { Department } from './../model/Department';
import { inject, bindable, customElement } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import {Service} from '../services/service';


@inject(HttpClient, Service)
export class CreateAsset{
    
  

  service: Service;
  asset: Asset;
  department = Department
  enumKeys = []
  message: string;
  showSuccessfulItem: boolean = false;
  showFailureItem: boolean = false;

  constructor(http, service){   
    this.enumKeys = Object.keys(this.department);    
    this.service = service;
  }

  createAsset(){    
    try {
      let newAsset = this.service.createAsset(this.asset);           
    } catch (error) {
      this.message = error.text();
      this.showFailureItem = true;
      window.setTimeout(() => {this.showFailureItem = false; this.message=""}, 3000);
    }finally{
      this.message = "Asset succesfuly created";
      this.showSuccessfulItem = true;
      window.setTimeout(() => {this.showSuccessfulItem = false; this.message=""}, 3000);
      this.asset = new Asset;
    }
     

    
  }
  
}

