import {
  ActiveGradeCell,
  selectCourseGradesState,
  setActiveColumn,
  setActiveGradeCell,
  setModalMode,
} from './course-grades.slice';
import { GradesModalMode } from './modals/grades-modal-mode.enum';
import { useAppDispatch, useAppSelector } from '../../hooks/redux-hooks';
import { AssessmentItem, GradeType } from '../../models/grade.interface';
import { MODAL_ANIMATION_DURATION } from '../../utils/antd/antd.constants';

export default function useCourseGradesState() {
  const appDispatch = useAppDispatch();

  const { activeColumn, activeGradeCell, modalMode } = useAppSelector(
    selectCourseGradesState,
  );

  const onOpenColumnModal = (assessment: AssessmentItem) => {
    appDispatch(setActiveColumn(assessment));

    if (assessment.type === GradeType.Manual) {
      appDispatch(setModalMode(GradesModalMode.EditManualColumn));
    }
  };

  const onOpenGradeModal = (state: ActiveGradeCell) => {
    appDispatch(setActiveGradeCell(state));
    appDispatch(setModalMode(GradesModalMode.SaveGrade));
  };

  const onCloseModal = () => {
    appDispatch(setModalMode(undefined));

    setTimeout(() => {
      appDispatch(setActiveGradeCell(undefined));
      appDispatch(setActiveColumn(undefined));
    }, MODAL_ANIMATION_DURATION);
  };

  return {
    activeColumn,
    activeGradeCell,
    modalMode,
    onOpenColumnModal,
    onOpenGradeModal,
    onCloseModal,
  };
}
