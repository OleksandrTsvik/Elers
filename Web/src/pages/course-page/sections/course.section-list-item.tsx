import { Divider, Typography } from 'antd';
import { useTranslation } from 'react-i18next';

import { HidableBadge } from '../../../components';
import { CourseTab } from '../../../models/course-tab.interface';
import CourseTabContent from '../tabs/course.tab-content';

import styles from './sections.module.scss';

interface Props {
  tab: CourseTab;
}

export default function CourseSectionListItem({ tab }: Props) {
  const { t } = useTranslation();

  return (
    <>
      <Divider />
      <HidableBadge
        color="lime"
        show={tab.showMaterialsCount}
        count={tab.materialCount}
        title={t('course.materials_count')}
      >
        <Typography.Title
          className={styles.sectionTitle}
          level={3}
          style={{ color: tab.color }}
        >
          {tab.name}
        </Typography.Title>
      </HidableBadge>
      <CourseTabContent materials={tab.courseMaterials} />
    </>
  );
}
