import { Space } from 'antd';
import { useTranslation } from 'react-i18next';

import TabSettingsDropdown from './tab.settings-dropdown';
import { HidableBadge, VisibilityIcon } from '../../../components';
import { CourseTab } from '../../../models/course-tab.interface';

interface Props {
  tab: CourseTab;
}

export default function TabListItem({ tab }: Props) {
  const { t } = useTranslation();

  return (
    <Space size="small">
      <VisibilityIcon
        visibility={tab.isActive}
        title={
          tab.isActive
            ? t('course_edit_page.visible_section')
            : t('course_edit_page.invisible_section')
        }
      />
      <HidableBadge
        size="small"
        color="lime"
        offset={[0, -7]}
        show={tab.showMaterialsCount}
        count={23}
        title={t('course.materials_count')}
      >
        <span style={{ color: tab.color }}>{tab.name}</span>
      </HidableBadge>
      <TabSettingsDropdown courseTab={tab} />
    </Space>
  );
}
