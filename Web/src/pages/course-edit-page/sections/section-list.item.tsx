import { Divider, Space, Typography } from 'antd';
import { useTranslation } from 'react-i18next';

import { HidableBadge, VisibilityIcon } from '../../../components';
import { CourseTab } from '../../../models/course-tab.interface';
import TabContent from '../tabs/tab.content';
import TabSettingsDropdown from '../tabs/tab.settings-dropdown';

import styles from './sections.module.scss';

interface Props {
  section: CourseTab;
}

export default function SectionListItem({ section }: Props) {
  const { t } = useTranslation();

  return (
    <>
      <Divider />
      <Space size="middle">
        <VisibilityIcon
          visibility={section.isActive}
          title={
            section.isActive
              ? t('course_edit_page.visible_section')
              : t('course_edit_page.invisible_section')
          }
        />
        <HidableBadge
          color="lime"
          show={section.showMaterialsCount}
          count={section.materialCount}
          title={t('course.materials_count')}
        >
          <Typography.Title
            className={styles.sectionTitle}
            level={3}
            style={{ color: section.color }}
          >
            {section.name}
          </Typography.Title>
        </HidableBadge>
        <TabSettingsDropdown courseTab={section} />
      </Space>
      <TabContent tabId={section.id} materials={section.courseMaterials} />
    </>
  );
}
