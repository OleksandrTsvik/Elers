import { useTranslation } from 'react-i18next';

import { CourseMaterialType } from './course-material.enum';

export function useMaterialLabels() {
  const { t } = useTranslation();

  const getMaterialLabel = (type: CourseMaterialType) => {
    switch (type) {
      case CourseMaterialType.Content:
        return t('course.material.content');
      case CourseMaterialType.Assignment:
        return t('course.material.assignment');
      case CourseMaterialType.File:
        return t('course.material.file');
      case CourseMaterialType.Test:
        return t('course.material.test');
      case CourseMaterialType.Link:
        return t('course.material.link');
      default:
        return t('course.material.default');
    }
  };

  return { getMaterialLabel };
}
