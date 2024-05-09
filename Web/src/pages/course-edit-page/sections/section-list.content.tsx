import { Empty } from 'antd';
import { useTranslation } from 'react-i18next';

import SectionListItem from './section-list.item';
import { CourseTab } from '../../../models/course-tab.interface';

interface Props {
  sections: CourseTab[];
}

export default function SectionListContent({ sections }: Props) {
  const { t } = useTranslation();

  if (sections.length === 0) {
    return (
      <Empty
        image={Empty.PRESENTED_IMAGE_SIMPLE}
        description={t('course_edit_page.no_sections')}
      />
    );
  }

  return sections.map((item) => (
    <SectionListItem key={item.id} section={item} />
  ));
}
