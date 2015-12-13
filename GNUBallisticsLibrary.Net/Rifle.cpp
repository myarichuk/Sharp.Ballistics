#pragma once
#include "Stdafx.h"
#include <stdlib.h>
#include "Rifle.h"

using namespace UnitsNet;
using namespace System::Collections::Generic;
namespace GNUBallisticsLibrary
{
	BallisticSolution^ Rifle::Solve(
		double shootingAngle,
		Speed windSpeed,
		double windAngle,
		Length range,
		WeatherCondition^ atmInfo,
		ShotLocationInfo^ shotLocationInfo) {

		if (rifleInfo == nullptr || ammoInfo == nullptr || scopeInfo == nullptr)
			throw gcnew System::Exception("Rifle is not initialized, run Initialize first");

		BallisticSolution^ shotInfo = gcnew BallisticSolution();

		double* solution;

		double bc = ammoInfo->BC;
		if (atmInfo != nullptr)
		{
			bc = AtmCorrect(ammoInfo->BC,
				rifleInfo->ZeroingConditions->Altitude.Feet,
				rifleInfo->ZeroingConditions->Barometer.Psi * 2.03602, //convert to Hg,
				rifleInfo->ZeroingConditions->Temperature.DegreesFahrenheit,
				rifleInfo->ZeroingConditions->RelativeHumidity);
		}

		auto latitude = NO_CORIOLIS;
		auto shotAzimuth = NO_CORIOLIS;

		if (shotLocationInfo != nullptr)
		{
			latitude = shotLocationInfo->Latitude;
			shotAzimuth = shotLocationInfo->ShotAzimuth;
		}

		try {
			SolveAll((int)ammoInfo->DragFunction,
				bc,
				ammoInfo->MuzzleVelocity.FeetPerSecond,
				scopeInfo->Height.Inches,
				shootingAngle,
				zeroAngle,
				windSpeed.MilesPerHour,
				windAngle,
				ammoInfo->Length.Inches,
				ammoInfo->Caliber.Inches,
				ammoInfo->WeightGrains,
				rifleInfo->BarrelTwist.Inches,
				latitude,
				shotAzimuth,
				&solution);

			double verticalClickMultiplier = 1 / scopeInfo->ElevationClicksPerMOA;
			double horizontalClickMultiplier = 1 / scopeInfo->WindageClicksPerMOA;
			double spinDrift = GetSpinDrift(solution, range.Yards);
			double windage = GetWindage(solution, range.Yards);

			shotInfo->BulletDrop = Length::FromInches(GetPath(solution, range.Yards));
			shotInfo->SpinDrift = Length::FromInches(spinDrift);
			shotInfo->WindDrift = Length::FromInches(windage - spinDrift);
			shotInfo->VerticalMOA = GetMOA(solution, range.Yards);
			shotInfo->HorizontalMOA = GetWindageMOA(solution, range.Yards);
			shotInfo->TimeToTargetSec = GetTime(solution, range.Yards);
			shotInfo->ImpactVelocity = Speed::FromFeetPerSecond(GetVelocity(solution, range.Yards));
			shotInfo->VerticalClicks = shotInfo->VerticalMOA * verticalClickMultiplier;
			shotInfo->HorizontalClicks = shotInfo->HorizontalMOA * horizontalClickMultiplier;
			shotInfo->Range = range;
		}
		finally{
			free(solution);
		}

		return shotInfo;
	}

	IEnumerable<BallisticSolution^>^ Rifle::SolveMultiple(double shootingAngle,
		Speed windSpeed,
		double windAngle,
		IEnumerable<Length>^ ranges,
		WeatherCondition^ atmInfo,
		ShotLocationInfo^ shotLocationInfo) {

		List<BallisticSolution^>^ results = gcnew List<BallisticSolution^>();

		for each(auto range in ranges)
			results->Add(Solve(shootingAngle, windSpeed, windAngle, range, atmInfo, shotLocationInfo));

		return results;
	}
}