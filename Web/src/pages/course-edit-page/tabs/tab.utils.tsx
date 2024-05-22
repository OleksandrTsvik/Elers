import { TabsProps } from 'antd';

import TabListItem from './tab-list.item';
import TabLoadMaterials from './tab.load-materials';
import { CourseTab } from '../../../models/course-tab.interface';
import { CourseMaterialType } from '../../../shared';

export function getTabsItems(tabs: CourseTab[]): TabsProps['items'] {
  return tabs.map((tab) => ({
    key: tab.id,
    label: <TabListItem tab={tab} />,
    children: <TabLoadMaterials tabId={tab.id} />,
    closable: false,
  }));
}

export function getCourseMaterialEditPagePath(
  type: CourseMaterialType,
  tabId: string,
  id: string,
): string {
  let typePath: string;

  switch (type) {
    case CourseMaterialType.Content:
      typePath = 'content';
      break;
    case CourseMaterialType.Assignment:
      typePath = 'assignment';
      break;
    case CourseMaterialType.File:
      typePath = 'file';
      break;
    case CourseMaterialType.Test:
      typePath = 'test';
      break;
    case CourseMaterialType.Link:
      typePath = 'link';
      break;
    default:
      typePath = 'default';
  }

  return `/courses/material/edit/${tabId}/${typePath}/${id}`;
}
