import { HttpClient} from 'aurelia-fetch-client';
import { inject } from 'aurelia-framework';
import { Asset } from "../model/asset";
import { Service } from "../services/service";

@inject(HttpClient, Service)
export class AssetList{

  assets : Asset[] = [];
  service : Service;
  
  constructor(http, service){
    this.service = service;  
    this.assets = this.service.getAssets();   
  }

  reloadAssets():void{
    this.assets = [];
    this.assets = this.service.getAssets(); 
  }
}
