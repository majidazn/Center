import { Component, Input, AfterViewInit, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import * as moment from 'jalali-moment';
import { CenterComponent } from '../center.component';
import { CenterService } from '../center.service';

@Component({
  selector: 'app-center-assigned-activity',
  templateUrl: './center-assigned-activity.component.html',
  styleUrls: ['./center-assigned-activity.component.scss']
})

export class CenterAssignedActivityComponent implements OnInit {
  showSpinner: boolean;
  centerActivityList: any;

  @Input() public parentInstance: CenterComponent;
  @Input() public centerId: any;

  constructor(private _centerService: CenterService, private _router: Router) {

  }

  ngOnInit(): void {
    this.getCenterActivities();
  }

  getCenterActivities() {

    this.showSpinner = true;
    return this._centerService.getCenterActivities(this.centerId).subscribe(
      res => {
        this.centerActivityList = [];
        res.forEach(item => {
          let acivity = {
            id: item.id,
            centerVariableName:item.centerVariableName,
            validFrom: item.validFrom ? moment(item.validFrom,'YYYY/MM/DD').locale('fa').format('YYYY/MM/DD'):'',
            validTo: item.validTo ? moment(item.validTo, 'YYYY/MM/DD').locale('fa').format('YYYY/MM/DD'):''
          }
          this.centerActivityList.push(acivity)
        })

        

        this.showSpinner = false;
      }
    )
  }

  deleteActivity(activityId) {
    let model = {
      activityId: activityId
    }
    return this._centerService.removeActivity(model).subscribe(
      res => {
         
        this.getCenterActivities();
      }
    )
  }
}
