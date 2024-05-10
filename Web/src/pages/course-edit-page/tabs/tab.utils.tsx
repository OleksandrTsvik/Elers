import { TabsProps } from 'antd';

import TabListItem from './tab-list.item';
import TabContent from './tab.content';
import { CourseTab } from '../../../models/course-tab.interface';

export function getTabsItems(tabs: CourseTab[]): TabsProps['items'] {
  return tabs.map((tab) => ({
    key: tab.id,
    label: <TabListItem tab={tab} />,
    children: <TabContent tab={tab} />,
    closable: false,
  }));
}
