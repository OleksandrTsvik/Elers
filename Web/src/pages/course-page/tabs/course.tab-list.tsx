import { Tabs } from 'antd';
import { useState } from 'react';

import { getTabsItems } from './tabs.utils';
import { CourseTab } from '../../../models/course-tab.interface';
import {
  CourseTabsEmpty,
  getCourseTabFromQueryParamOrFirst,
  setCourseTabToQueryParam,
} from '../../../shared';

interface Props {
  tabs: CourseTab[];
}

export default function CourseTabList({ tabs }: Props) {
  const [activeTab, setActiveTab] = useState<string>();

  const items = getTabsItems(tabs);

  const handleChange = (activeKey: string) => {
    setActiveTab(activeKey);
    setCourseTabToQueryParam(activeKey);
  };

  if (!tabs.length) {
    return <CourseTabsEmpty />;
  }

  return (
    <Tabs
      destroyInactiveTabPane
      type="card"
      activeKey={activeTab ?? getCourseTabFromQueryParamOrFirst(tabs)}
      items={items}
      onChange={handleChange}
    />
  );
}
