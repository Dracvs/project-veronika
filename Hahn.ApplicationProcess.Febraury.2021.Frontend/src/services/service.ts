import { HttpClient, json } from 'aurelia-fetch-client';
import { inject } from 'aurelia-framework';
import {Asset} from '../model/asset';
import * as environment from '../../config/environment.json';

@inject(HttpClient)
export class  Service{

  resultSet : Asset[] = [];
  asset: Asset;
  public http: HttpClient;

  constructor(http){
    this.http = http;
    const baseUrl = environment.api_url;
    http.configure(config=>
      config.withBaseUrl(baseUrl));
  }

  getAssets(): Asset[]{    
    this.resultSet = [];
    this.http.fetch("")
      .then(response => response.json())
      .then(response =>         
        response.map((asset:any)=> this.formatAssetList(asset, this.resultSet))
      )
      .catch(error =>{
        console.log('Error retrieving assets')
      });
      return this.resultSet;
  }

  createAsset(asset: Asset): Asset{    
    this.asset = new Asset;
    this.http.fetch("", {
      method: 'post',
      body: json(asset)
    })
    .then(response => {
      if(response.status == 400 || response.status == 500)
      {
        throw new Error(response.body.getReader.toString());
      }
      response.json();
    })
    .then(createdAsset => {
      this.asset = this.formatAsset(createdAsset);
    })
    .catch(error => {
      if(error instanceof Response){
        error.text().then(text => {
          console.log('Error: ' + text);
          throw new Error(text);
        });
      }
    });
    return this.asset;
  }

  formatAssetList(asset:any, assetList: Asset[]){
    assetList.push({
      AssetName: asset.assetName, 
      Id: asset.id, 
      Broken: asset.broken,
      CountryOfDepartment: asset.countryOfDepartment,
      Department: asset.department,
      EmailAddressOfDepartment: asset.emailAddressOfDepartment,
      PurchaseDate: asset.purchaseDate
    });
  }

  formatAsset(asset:any):Asset{
    return {
      AssetName: asset.assetName, 
      Id: asset.id, 
      Broken: asset.broken,
      CountryOfDepartment: asset.countryOfDepartment,
      Department: asset.department,
      EmailAddressOfDepartment: asset.emailAddressOfDepartment,
      PurchaseDate: asset.purchaseDate
    };
  }
}
