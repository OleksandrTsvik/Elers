import { useTranslation } from 'react-i18next';

import { CourseMaterialBreadcrumb } from '../../shared';

interface Props {
  courseId: string;
  title: string;
}

export default function MaterialLinkCreationBreadcrumb({
  courseId,
  title,
}: Props) {
  const { t } = useTranslation();

  return (
    <CourseMaterialBreadcrumb
      courseId={courseId}
      courseTitle={title}
      title={t('material_link_creation_page.title')}
    />
  );
}
