import moment from '../../node_modules/moment';

export class DateFormatValueConverter{
  toView(value){
    return moment(value).format('YYYY-MM-DD');
  }
}
