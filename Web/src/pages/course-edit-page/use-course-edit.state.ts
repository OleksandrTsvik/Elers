import {
  selectCourseEditState,
  setActiveCourseMaterial,
  setActiveCourseTab,
  setModalMode,
} from './course-edit.slice';
import { useAppDispatch, useAppSelector } from '../../hooks/redux-hooks';
import { MODAL_ANIMATION_DURATION } from '../../utils/antd/antd.constants';

export default function useCourseEditState() {
  const appDispatch = useAppDispatch();

  const { activeCourseTab, activeCourseMaterial, modalMode } = useAppSelector(
    selectCourseEditState,
  );

  const onCloseModal = () => {
    appDispatch(setModalMode(undefined));

    setTimeout(() => {
      appDispatch(setActiveCourseTab(undefined));
      appDispatch(setActiveCourseMaterial(undefined));
    }, MODAL_ANIMATION_DURATION);
  };

  return { activeCourseTab, activeCourseMaterial, modalMode, onCloseModal };
}
