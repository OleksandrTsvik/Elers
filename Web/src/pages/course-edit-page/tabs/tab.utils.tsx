import { TabsProps } from 'antd';

import TabListItem from './tab-list.item';
import TabLoadMaterials from './tab.load-materials';
import { CourseTab } from '../../../models/course-tab.interface';

export function getTabsItems(tabs: CourseTab[]): TabsProps['items'] {
  return tabs.map((tab) => ({
    key: tab.id,
    label: <TabListItem tab={tab} />,
    children: <TabLoadMaterials tabId={tab.id} />,
    closable: false,
  }));
}
