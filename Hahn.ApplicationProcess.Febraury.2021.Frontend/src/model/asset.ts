import { Department } from './Department';


export class Asset
{
  public Id: number;
  public AssetName: string;
  public Department: Department;
  public CountryOfDepartment: string;
  public EmailAddressOfDepartment: string;
  public PurchaseDate: Date;
  public Broken: boolean;
//  public DepartmentType = Department;
}


