import { theme } from 'antd';
import { useTranslation } from 'react-i18next';

import { HidableBadge } from '../../../components';
import { CourseTab } from '../../../models/course-tab.interface';

interface Props {
  tab: CourseTab;
}

export default function CourseTabListItem({ tab }: Props) {
  const { t } = useTranslation();

  const {
    token: { colorText },
  } = theme.useToken();

  return (
    <HidableBadge
      size="small"
      color="lime"
      offset={[0, -3]}
      show={tab.showMaterialsCount}
      count={tab.materialCount}
      title={t('course.materials_count')}
    >
      <span style={{ color: tab.color ?? colorText }}>{tab.name}</span>
    </HidableBadge>
  );
}
