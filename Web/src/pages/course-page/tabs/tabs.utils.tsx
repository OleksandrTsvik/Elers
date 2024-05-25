import { TabsProps } from 'antd';

import CourseTabListItem from './course.tab-list-item';
import CourseTabLoadMaterials from './course.tab-load-materials';
import { CourseTab } from '../../../models/course-tab.interface';

export function getTabsItems(tabs: CourseTab[]): TabsProps['items'] {
  return tabs.map((tab) => ({
    key: tab.id,
    label: <CourseTabListItem tab={tab} />,
    children: <CourseTabLoadMaterials tabId={tab.id} />,
    closable: false,
  }));
}
