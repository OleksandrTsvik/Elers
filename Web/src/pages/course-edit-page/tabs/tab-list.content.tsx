import { Tabs } from 'antd';
import { useEffect, useState } from 'react';

import { getTabFromQueryOrFirst, setTab } from './tab-query-params.utils';
import { getTabsItems } from './tab.utils';
import { useAppDispatch } from '../../../hooks/redux-hooks';
import { CourseTab } from '../../../models/course-tab.interface';
import { setModalMode } from '../course-edit.slice';
import { CourseTabModalMode } from '../modals/tab-modal-mode.enum';

interface Props {
  tabs: CourseTab[];
}

export default function TabListContent({ tabs }: Props) {
  const [activeTab, setActiveTab] = useState<string>();

  const appDispatch = useAppDispatch();
  const items = getTabsItems(tabs);

  useEffect(() => {
    setActiveTab(undefined);
  }, [tabs]);

  const handleChange = (activeKey: string) => {
    setActiveTab(activeKey);
    setTab(activeKey);
  };

  const handleEdit = (action: 'add' | 'remove') => {
    if (action === 'add') {
      appDispatch(setModalMode(CourseTabModalMode.CreateTab));
    }
  };

  return (
    <Tabs
      destroyInactiveTabPane
      type="editable-card"
      activeKey={activeTab ?? getTabFromQueryOrFirst(tabs)}
      items={items}
      onChange={handleChange}
      onEdit={(_, action) => handleEdit(action)}
    />
  );
}
