import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageBillComponent } from './manage-bill.component';

describe('ManageBillComponent', () => {
  let component: ManageBillComponent;
  let fixture: ComponentFixture<ManageBillComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageBillComponent]
    });
    fixture = TestBed.createComponent(ManageBillComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
